using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.ListPaginated;

public sealed record ListPaginatedProjectsRequest(int Skip, int Take) : IRequest<Result<PageDto<Project>>>;

public sealed class ListPaginatedProjectsValidator
    : AbstractValidator<ListPaginatedProjectsRequest>
{
    public ListPaginatedProjectsValidator()
    {
        this.RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        this.RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
    }
}
