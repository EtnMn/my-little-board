using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Aggregates.Clients.Specifications;

namespace Etn.MyLittleBoard.Application.Clients.ListPaginated;

public sealed class ListPaginatedClientsHandler(
    IRepository<Client> repository) :
    IRequestHandler<ListPaginatedClientsRequest, Result<PageDto<Client>>>
{
    public async Task<Result<PageDto<Client>>> Handle(
        ListPaginatedClientsRequest request,
        CancellationToken cancellationToken)
    {
        ClientsPaginated specification = new(
            request.Search,
            request.Skip,
            request.Take,
            request.Descending,
            request.ExcludeDisabled);

        int count = await repository.CountAsync(specification, cancellationToken);
        List<Client> result = await repository.ListAsync(specification, cancellationToken);

        return new PageDto<Client>([.. result], request.Skip, request.Take, count);
    }
}
