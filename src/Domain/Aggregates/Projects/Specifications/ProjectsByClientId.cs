using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectsByClientId : Specification<Project>
{
    public ProjectsByClientId(ProjectClientId clientId)
    {
        this.Query.Where(p => p.ClientId == clientId);
    }
}
