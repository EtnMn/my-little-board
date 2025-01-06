using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;
public sealed class ClientByIdNoTracking : Specification<Client>
{
    public ClientByIdNoTracking(ClientId clientId)
    {
        this.Query.Where(client => client.Id == clientId).AsNoTracking();
    }
}
