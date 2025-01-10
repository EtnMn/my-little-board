using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.Application.Clients.EditState;
public sealed class EditClientStateHandler(
    IRepository<Client> repository,
    IUserService userService) :
    IRequestHandler<EditClientStateRequest, Result>
{
    public async Task<Result> Handle(EditClientStateRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Client? client = await repository.GetByIdAsync(ClientId.From(request.ClientId), cancellationToken);
        if (client is not null)
        {
            if (ClientState.Enabled == request.Enable)
            {
                client.Enable();
            }
            else
            {
                client.Disable();
            }

            await repository.UpdateAsync(client, cancellationToken);
            return await Task.FromResult(Result.Success());
        }
        else
        {
            return Result.NotFound();
        }
    }
}
