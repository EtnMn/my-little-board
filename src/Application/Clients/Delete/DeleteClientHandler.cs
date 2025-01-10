using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Events;

namespace Etn.MyLittleBoard.Application.Clients.Delete;

public sealed class DeleteClientHandler(
    IRepository<Client> repository,
    IUserService userService,
    IPublisher publisher) :
    IRequestHandler<DeleteClientRequest, Result>
{
    public async Task<Result> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        Client? client = await repository.GetByIdAsync(ClientId.From(request.ClientId), cancellationToken);
        if (client is not null)
        {
            await repository.DeleteAsync(client, cancellationToken);
            await publisher.Publish(new ClientDeletedEvent(client.Id), cancellationToken);
            return await Task.FromResult(Result.Success());
        }
        else
        {
            return Result.NotFound();
        }
    }
}
