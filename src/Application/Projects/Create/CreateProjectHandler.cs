using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using MediatR;

namespace Etn.MyLittleBoard.Application.Projects.Create;

public sealed class CreateProjectHandler(
    IRepository<Project> repository) :
    IRequestHandler<CreateProjectRequest, Result<ProjectId>>
{
    public async Task<Result<ProjectId>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        Project project = new(ProjectName.From(request.Name));
        await repository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}
