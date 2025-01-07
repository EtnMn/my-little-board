using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients.Specifications;

public sealed class ClientByIdNoTrackingConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Filter_Client_By_Id()
    {
        int i = 0;
        IEnumerable<Client> clients = this.fixture
          .Build<Client>()
          .FromFactory(() => new Client(ClientName.From(this.fixture.Create<string>()), ClientNote.Unspecified))
          .With(p => p.Id, () => ClientId.From(++i))
          .CreateMany(this.fixture.Create<int>());

        ClientByIdNoTracking specification = new(clients.First().Id);
        IEnumerable<Client> result = specification.Evaluate(clients);

        result.Should().ContainSingle();
        result.First().Id.Should().Be(clients.First().Id);
    }
}
