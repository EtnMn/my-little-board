using Etn.MyLittleBoard.Domain.Aggregates.Clients;
using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Clients.Create;

public sealed class CreateClientRequest() : IRequest<Result<ClientId>>
{
    public string Name { get; set; } = string.Empty;

}

public sealed class CreateClientValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultTextLength);
    }
}
