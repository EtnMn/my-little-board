using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Projects.Edit;

public sealed class EditProjectRequest(int projectId) : IRequest<Result>
{
    public int ProjectId { get; set; } = projectId;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public int? ClientId { get; set; }
}

public sealed class EditProjectValidator : AbstractValidator<EditProjectRequest>
{
    public EditProjectValidator()
    {
        this.RuleFor(x => x.ProjectId).GreaterThan(0);
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Description).MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Color).Matches(ValidationConstants.HexColorRegex).When(x => !string.IsNullOrWhiteSpace(x.Color));
        this.RuleFor(x => x.Start).LessThanOrEqualTo(x => x.End).When(x => x.Start.HasValue && x.End.HasValue);
        this.RuleFor(x => x.End).GreaterThanOrEqualTo(x => x.Start).When(x => x.End.HasValue && x.Start.HasValue);
        this.RuleFor(x => x.ClientId).GreaterThanOrEqualTo(0).When(x => x.ClientId.HasValue);
    }
}
