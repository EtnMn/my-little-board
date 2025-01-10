using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Clients.Edit;

public sealed class EditClientRequest(int clientId) : IRequest<Result>
{
    public int ClientId { get; set; } = clientId;

    public string Name { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;
}

public sealed class EditClientValidator : AbstractValidator<EditClientRequest>
{
    public EditClientValidator()
    {
        this.RuleFor(x => x.ClientId).GreaterThan(0);
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Note).MaximumLength(ValidationConstants.LongTextLength);
    }
}
