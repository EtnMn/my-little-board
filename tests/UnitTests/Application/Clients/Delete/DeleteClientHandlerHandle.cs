using Etn.MyLittleBoard.Application.Clients.Delete;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Events;
using Etn.MyLittleBoard.Domain.Interfaces;
using MediatR;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.Delete;

public sealed class DeleteClientHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Client> repository;
    private readonly IUserService userService;
    private readonly IPublisher publisher;

    public DeleteClientHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Client>>();
        this.userService = Substitute.For<IUserService>();
        this.publisher = Substitute.For<IPublisher>();
    }

    [Fact]
    public async Task Should_Delete_Client()
    {
        Client client = this.fixture.Build<Client>()
            .FromFactory(() => new Client(
                ClientName.From(this.fixture.Create<string>()),
                ClientNote.Unspecified))
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        this.userService
            .AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        this.repository.GetByIdAsync(client.Id, Arg.Any<CancellationToken>()).Returns(client);

        DeleteClientHandler handler = new(this.repository, this.userService, this.publisher);
        DeleteClientRequest request = new(client.Id.Value);
        Result result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await this.repository.Received().DeleteAsync(client, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Publish_Deleted_Client_Event()
    {
        Client client = this.fixture.Build<Client>()
            .FromFactory(() => new Client(
                ClientName.From(this.fixture.Create<string>()),
                ClientNote.Unspecified))
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        this.userService
            .AuthenticatedUser
            .Returns(this.fixture.Build<User>().With(x => x.Administrator, true)
            .Create());

        this.repository.GetByIdAsync(client.Id, Arg.Any<CancellationToken>()).Returns(client);

        DeleteClientHandler handler = new(this.repository, this.userService, this.publisher);
        DeleteClientRequest request = new(client.Id.Value);
        Result result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        client.State.Should().Be(ClientState.Enabled);
        await this.publisher.Received().Publish(Arg.Any<ClientDeletedEvent>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Return_Unauthorized_When_No_Authenticated()
    {
        DeleteClientHandler handler = new(this.repository, this.userService, this.publisher);
        DeleteClientRequest request = new(this.fixture.Create<int>());
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_No_Administrator()
    {
        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());

        this.userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());
        DeleteClientHandler handler = new(this.repository, this.userService, this.publisher);
        DeleteClientRequest request = new(this.fixture.Create<int>());
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

        DeleteClientHandler handler = new(this.repository, this.userService, this.publisher);
        DeleteClientRequest request = new(this.fixture.Create<int>());
        Result result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }
}
