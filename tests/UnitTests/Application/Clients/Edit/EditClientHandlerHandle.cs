using Etn.MyLittleBoard.Application.Clients.Edit;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.Edit;

public sealed class EditClientHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Client> repository;
    private readonly IUserService userService;
    private readonly ICachedService cachedService;

    public EditClientHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Client>>();
        this.userService = Substitute.For<IUserService>();
        this.cachedService = Substitute.For<ICachedService>();
    }

    [Fact]
    public async Task Should_Update_Client_Values()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        Client client = this.fixture
            .Build<Client>()
            .FromFactory<int>((x) => new Client(
                ClientName.From(this.fixture.Create<string>()),
                ClientNote.Unspecified))
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        this.repository
            .GetByIdAsync(client.Id, Arg.Any<CancellationToken>())
            .Returns(client);

        EditClientHandler handler = new(this.repository, this.userService, this.cachedService);
        EditClientRequest request = new(client.Id.Value)
        {
            Name = this.fixture.Create<string>(),
            Note = this.fixture.Create<string>(),
        };

        Result result = await handler.Handle(request, CancellationToken.None);

        _ = this.userService.Received().AuthenticatedUser;
        await this.repository.Received().GetByIdAsync(Arg.Any<ClientId>(), Arg.Any<CancellationToken>());
        await this.repository.Received().UpdateAsync(client, Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Set_Client_In_Cache()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());
        Client client = this.fixture
           .Build<Client>()
           .FromFactory<int>((x) => new Client(
               ClientName.From(this.fixture.Create<string>()),
               ClientNote.Unspecified))
           .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
           .Create();

        this.repository
            .GetByIdAsync(client.Id, Arg.Any<CancellationToken>())
            .Returns(client);

        EditClientHandler handler = new(this.repository, this.userService, this.cachedService);
        EditClientRequest request = new(client.Id.Value)
        {
            Name = this.fixture.Create<string>(),
        };

        _ = await handler.Handle(request, CancellationToken.None);

        string[] tags = ["clients"];
        await this.cachedService.Received().Set(
            $"client-{client.Id}",
            Arg.Any<Client>(),
            Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(tags)),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Client_Does_Not_Exist()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        EditClientHandler handler = new(this.repository, this.userService, this.cachedService);
        EditClientRequest request = new(this.fixture.Create<int>());
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task Should_Return_Unauthorized_When_User_Not_Authenticated()
    {
        EditClientHandler handler = new(this.repository, this.userService, this.cachedService);
        EditClientRequest request = new(this.fixture.Create<int>());
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_User_Not_Administrator()
    {
        this.userService.AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, false)
            .Create());

        EditClientHandler handler = new(this.repository, this.userService, this.cachedService);
        EditClientRequest request = new(this.fixture.Create<int>());
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}
