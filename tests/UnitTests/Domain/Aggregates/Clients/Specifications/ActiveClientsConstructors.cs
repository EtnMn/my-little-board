using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients.Specifications;

public sealed class ActiveClientsConstructors
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Filter_Active_Clients()
    {
        int i = 0;
        IEnumerable<Client> projects = this.fixture
            .Build<Client>()
            .FromFactory(() =>
            {
                Client client = new(
                    ClientName.From(this.fixture.Create<string>()),
                    ClientNote.Unspecified);

                if (++i % 2 == 0)
                {
                    client.Disable();
                }

                return client;
            })
            .CreateMany();

        ActiveClients specification = new();
        IEnumerable<Client> result = specification.Evaluate(projects);

        result.Should().HaveCount(projects.Count(p => p.State == ClientState.Enabled));
    }
}
