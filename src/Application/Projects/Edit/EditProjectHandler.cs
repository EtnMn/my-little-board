using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.Edit;

public sealed class EditProjectHandler(
    IRepository<Project> repository,
    IUserService userService) :
    IRequestHandler<EditProjectRequest, Result>
{
    public async Task<Result> Handle(EditProjectRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Project? project = await repository.GetByIdAsync(ProjectId.From(request.ProjectId), cancellationToken);
        if (project is not null)
        {
            project.UpdateName(ProjectName.From(request.Name));

            await repository.UpdateAsync(project, cancellationToken);
            return Result.Success();
        }
        else
        {
            return Result.NotFound();
        }
    }
}
