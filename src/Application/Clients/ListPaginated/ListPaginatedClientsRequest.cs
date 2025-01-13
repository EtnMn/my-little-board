using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Clients.ListPaginated;

public sealed record ListPaginatedClientsRequest(string Search, int Skip, int Take, bool Descending) :
    IRequest<Result<PageDto<Client>>>
{
    public bool ExcludeDisabled { get; set; } = true;
}

public sealed class ListPaginatedClientsValidator
    : AbstractValidator<ListPaginatedClientsRequest>
{
    public ListPaginatedClientsValidator()
    {
        this.RuleFor(x => x.Search).NotNull().MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        this.RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
    }
}
