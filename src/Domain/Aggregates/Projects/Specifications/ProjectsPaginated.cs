using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectsPaginated : Specification<Project>
{
    public ProjectsPaginated(int skip, int take)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(skip, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(take, 0);

        this.Query.OrderBy(p => p.Name).Skip(skip).Take(take);
    }
}
