using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.Application.Projects.ListPaginated;

public sealed class ListPaginatedProjectsHandler(
    IRepository<Project> repository) :
    IRequestHandler<ListPaginatedProjectsRequest, Result<PageDto<Project>>>
{
    public async Task<Result<PageDto<Project>>> Handle(
        ListPaginatedProjectsRequest request,
        CancellationToken cancellationToken)
    {
        ProjectsPaginated specification = new(request.Skip, request.Take);

        int count = await repository.CountAsync(specification, cancellationToken);
        List<Project> result = await repository.ListAsync(specification, cancellationToken);

        return new PageDto<Project>([.. result], request.Skip, request.Take, count);
    }
}
