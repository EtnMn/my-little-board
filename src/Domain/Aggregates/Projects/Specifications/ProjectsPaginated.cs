using Ardalis.Specification;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

public sealed class ProjectsPaginated : Specification<Project, Project>
{
    public ProjectsPaginated(int skip, int take)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(skip, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(take, 0);

        this.Query
            .Select(p =>
                new Project(
                    p.Name,
                    ProjectDescription.From(new string(p.Description.Value.Take(100).ToArray())),
                    p.Color,
                    p.Status)
                {
                    Id = p.Id,
                })
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(skip)
            .Take(take);
    }
}
