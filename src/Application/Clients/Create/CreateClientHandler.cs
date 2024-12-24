using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.Application.Clients.Create;

public sealed class CreateClientHandler(
    IRepository<Client> repository,
    IUserService userService) :
    IRequestHandler<CreateClientRequest, Result<ClientId>>

{
    public async Task<Result<ClientId>> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result<ClientId>.Unauthorized() : Result<ClientId>.Forbidden();
        }

        Client client = new(ClientName.From(request.Name), ClientNote.Unspecified);
        client = await repository.AddAsync(client, cancellationToken);

        return client.Id;
    }
}
