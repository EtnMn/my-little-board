using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.Application.Clients.Edit;

public sealed class EditClientHandler(
    IRepository<Client> repository,
    IUserService userService) :
    IRequestHandler<EditClientRequest, Result>
{
    public async Task<Result> Handle(EditClientRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Client? client = await repository.GetByIdAsync(ClientId.From(request.ClientId), cancellationToken);
        if (client is not null)
        {
            client.UpdateName(ClientName.From(request.Name));
            client.UpdateNote(ClientNote.From(request.Note));

            await repository.UpdateAsync(client, cancellationToken);
            return Result.Success();
        }
        else
        {
            return Result.NotFound();
        }
    }
}
