using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.Create;

public sealed class CreateProjectHandler(
    IRepository<Project> repository,
    IUserService userService) :
    IRequestHandler<CreateProjectRequest, Result<ProjectId>>
{
    public async Task<Result<ProjectId>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result<ProjectId>.Unauthorized() : Result<ProjectId>.Forbidden();
        }

        Project project = new(
            ProjectName.From(request.Name),
            !string.IsNullOrWhiteSpace(request.Description) ?
                ProjectDescription.From(request.Description) :
                ProjectDescription.Unspecified);

        project = await repository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}
