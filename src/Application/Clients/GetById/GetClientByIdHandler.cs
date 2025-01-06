using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.Application.Clients.GetById;

public sealed class GetClientByIdHandler(IRepository<Client> repository) :
    IRequestHandler<GetClientByIdRequest, Result<Client>>
{
    public async Task<Result<Client>> Handle(GetClientByIdRequest request, CancellationToken cancellationToken)
    {
        ClientByIdNoTracking specification = new(ClientId.From(request.Id));
        Client? client = await repository.FirstOrDefaultAsync(specification, cancellationToken);
        if (client is not null)
        {
            return client;
        }
        else
        {
            return Result<Client>.NotFound();
        }
    }
}

