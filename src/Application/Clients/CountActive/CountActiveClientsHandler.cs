using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.Application.Clients.CountActive;

public sealed class CountActiveClientsHandler(
    IRepository<Client> repository,
    IUserService userService) :
    IRequestHandler<CountActiveClientsRequest, Result<int>>
{
    public async Task<Result<int>> Handle(CountActiveClientsRequest request, CancellationToken cancellationToken)
    {
        if (userService.AuthenticatedUser is null || !userService.AuthenticatedUser.Administrator)
        {
            return userService.AuthenticatedUser is null ? Result.Unauthorized() : Result.Forbidden();
        }

        ActiveClients specification = new();
        return await repository.CountAsync(specification, cancellationToken);
    }
}

