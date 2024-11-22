using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.Application.Projects.ListPaginated;

public sealed class ListPaginatedProjectsHandler(
    IRepository<Project> repository) :
    IRequestHandler<ListPaginatedProjectsRequest, Result<Project[]>>
{
    public async Task<Result<Project[]>> Handle(
        ListPaginatedProjectsRequest request,
        CancellationToken cancellationToken)
    {
        ProjectsPaginated specification = new(0, 10);
        return await repository.GetAllAsync(specification, cancellationToken);
    }
}
