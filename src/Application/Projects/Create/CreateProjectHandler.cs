using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using MediatR;

namespace Etn.MyLittleBoard.Application.Projects.Create;

internal sealed class CreateProjectHandler(
    IAppDbContext dbContext) :
    IRequestHandler<CreateProjectRequest, Result<ProjectId>>
{
    public async Task<Result<ProjectId>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        Project project = new(ProjectName.From(request.Name));
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
