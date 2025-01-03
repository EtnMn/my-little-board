using Etn.MyLittleBoard.Application.Clients.Create;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.Create;

public sealed class CreateClientHandlerHandle
{
    private readonly Fixture fixture = new();
    private readonly IRepository<Client> repository;
    private readonly int clientId;
    private readonly string clientName;

    public CreateClientHandlerHandle()
    {
        this.repository = Substitute.For<IRepository<Client>>();
        this.clientId = this.fixture.Create<int>();
        this.clientName = this.fixture.Create<string>();
        this.repository.AddAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>()).Returns(
            new Client(ClientName.From(this.clientName), ClientNote.Unspecified)
            {
                Id = ClientId.From(this.clientId)
            });
    }

    [Fact]
    public async Task Should_Create_Client()
    {
        IUserService userService = Substitute.For<IUserService>();
        userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, true).Create());
        CreateClientHandler handler = new(this.repository, userService);
        CreateClientRequest request = new() { Name = this.clientName };
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<ClientId>();
        await this.repository.Received().AddAsync(Arg.Any<Client>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_Return_Unauthorized_When_No_Authenticated()
    {
        IUserService userService = Substitute.For<IUserService>();
        CreateClientHandler handler = new(this.repository, userService);
        CreateClientRequest request = new();
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_No_Administrator()
    {
        IUserService userService = Substitute.For<IUserService>();
        userService.AuthenticatedUser.Returns(this.fixture.Build<User>().With(x => x.Administrator, false).Create());
        CreateClientHandler handler = new(this.repository, userService);
        CreateClientRequest request = new();
        Result<ClientId> result = await handler.Handle(request, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }
}
