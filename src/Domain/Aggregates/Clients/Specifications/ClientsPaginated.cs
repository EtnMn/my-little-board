using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

public sealed class ClientsPaginated : Specification<Client>
{
    public ClientsPaginated(string search, int skip, int take, bool descending, bool excludeDisabled)
    {
        ArgumentNullException.ThrowIfNull(search);
        ArgumentOutOfRangeException.ThrowIfLessThan(skip, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(take, 0);

        this.Query.AsNoTracking();

        if (excludeDisabled)
        {
            this.Query.Where(x => x.State != ClientState.Disabled);
        }

        if (descending)
        {
            this.Query.OrderByDescending(x => x.Name);
        }
        else
        {
            this.Query.OrderBy(x => x.Name);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            this.Query.Search(x => (string)x.Name, $"%{search}%");
        }

        this.Query.Skip(skip).Take(take);
    }
}
