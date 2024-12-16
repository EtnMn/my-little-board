using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectByIdNoTracking : Specification<Project>
{
    public ProjectByIdNoTracking(ProjectId projectId)
    {
        this.Query.Where(project => project.Id == projectId).AsNoTracking();
    }
}
