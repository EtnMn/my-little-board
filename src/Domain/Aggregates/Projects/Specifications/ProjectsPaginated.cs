using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectsPaginated : Specification<Project>
{
    public ProjectsPaginated(int skip, int take)
    {
        this.Query.OrderBy(p => p.Name).Skip(skip).Take(take);
    }
}
