using Etn.MyLittleBoard.Application.Clients.GetById;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Application.Clients.GetById;

public sealed class GetClientByIdHandlerHandle
{
    private readonly Fixture fixture = new();

    [Fact]
    public async Task Should_Get_Specified_Client_Or_Null()
    {
        Client client = this.fixture
            .Build<Client>()
            .With(x => x.Id, ClientId.From(this.fixture.Create<int>()))
            .Create();

        IRepository<Client> repository = Substitute.For<IRepository<Client>>();
        repository
            .FirstOrDefaultAsync(Arg.Any<ClientByIdNoTracking>(), Arg.Any<CancellationToken>())
            .Returns(client);

        GetClientByIdHandler handler = new(repository);
        GetClientByIdRequest request = new(client.Id.Value);
        Result<Client> result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(client);
        await repository
            .Received()
            .FirstOrDefaultAsync(Arg.Any<ClientByIdNoTracking>(), Arg.Any<CancellationToken>());
    }
}
