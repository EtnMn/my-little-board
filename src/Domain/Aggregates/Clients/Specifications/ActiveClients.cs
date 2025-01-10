using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

public sealed class ActiveClients : Specification<Client>
{
    public ActiveClients()
    {
        this.Query.Where(client => client.State == ClientState.Enabled);
    }
}
