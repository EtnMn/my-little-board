using Etn.MyLittleBoard.Application.Clients.EditState;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.EditState;

public sealed class EditClientStateHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Client> repository;
    private readonly IUserService userService;

    public EditClientStateHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Client>>();
        this.userService = Substitute.For<IUserService>();
    }

    [Fact]
    public async Task Should_Enable_Client()
    {
        Client client = this.fixture.Build<Client>()
            .FromFactory(() => new Client(
                ClientName.From(this.fixture.Create<string>()),
                ClientNote.Unspecified))
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());
        this.repository.GetByIdAsync(client.Id, Arg.Any<CancellationToken>()).Returns(client);

        EditClientStateHandler handler = new(this.repository, this.userService);
        EditClientStateRequest request = new(client.Id.Value, true);
        Result result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        client.State.Should().Be(ClientState.Enabled);
        await this.repository.Received().UpdateAsync(client, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Disable_Client()
    {
        Client client = this.fixture.Build<Client>()
            .FromFactory(() => new Client(
                ClientName.From(this.fixture.Create<string>()),
                ClientNote.Unspecified))
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        this.repository.GetByIdAsync(client.Id, Arg.Any<CancellationToken>()).Returns(client);

        EditClientStateHandler handler = new(this.repository, this.userService);
        EditClientStateRequest request = new(client.Id.Value, false);
        Result result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        client.State.Should().Be(ClientState.Disabled);
        await this.repository.Received().UpdateAsync(client, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Return_Unauthorized_When_No_Authenticated()
    {
        EditClientStateHandler handler = new(this.repository, this.userService);
        EditClientStateRequest request = new(this.fixture.Create<int>(), this.fixture.Create<bool>());
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_No_Administrator()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, false)
            .Create());

        EditClientStateHandler handler = new(this.repository, this.userService);
        EditClientStateRequest request = new(this.fixture.Create<int>(), this.fixture.Create<bool>());
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Client_Does_Not_Exist()
    {
        this.userService
            .AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        this.repository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns((Client?)null);

        EditClientStateHandler handler = new(this.repository, this.userService);
        EditClientStateRequest request = new(this.fixture.Create<int>(), this.fixture.Create<bool>());
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }
}
