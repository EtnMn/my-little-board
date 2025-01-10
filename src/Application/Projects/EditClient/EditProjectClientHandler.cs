using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.EditClient;

public sealed class EditProjectClientHandler(
    IRepository<Project> repository,
    IUserService userService) :
    IRequestHandler<EditProjectClientRequest, Result>
{
    public async Task<Result> Handle(EditProjectClientRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Project? project = await repository.GetByIdAsync(ProjectId.From(request.ProjectId), cancellationToken);
        if (project is not null)
        {
            if (request.ClientId.HasValue && request.ClientId.Value > 0)
            {
                project.SetClient(ProjectClientId.From(request.ClientId.Value));
            }
            else
            {
                project.RemoveClient();
            }

            await repository.UpdateAsync(project, cancellationToken);

            return await Task.FromResult(Result.Success());
        }
        else
        {
            return Result.NotFound();
        }
    }
}
