namespace Etn.MyLittleBoard.Application.Projects.EditClient;

public sealed record EditProjectClientRequest(int ProjectId, int? ClientId) : IRequest<Result>;

public sealed class EditProjectClientValidator : AbstractValidator<EditProjectClientRequest>
{
    public EditProjectClientValidator()
    {
        this.RuleFor(x => x.ProjectId).GreaterThan(0);
        this.RuleFor(x => x.ClientId).GreaterThanOrEqualTo(0).When(x => x.ClientId.HasValue);
    }
}
