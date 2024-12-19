using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.EditStatus;

public sealed class EditProjectStatusHandler(
    IRepository<Project> repository,
    IUserService userService) :
    IRequestHandler<EditProjectStatusRequest, Result>
{
    public async Task<Result> Handle(EditProjectStatusRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Project? project = await repository.GetByIdAsync(ProjectId.From(request.ProjectId), cancellationToken);
        if (project is not null)
        {
            project.UpdateStatus(request.ProjectStatus);
            await repository.UpdateAsync(project, cancellationToken);

            return await Task.FromResult(Result.Success());
        }
        else
        {
            return Result.NotFound();
        }
    }
}
