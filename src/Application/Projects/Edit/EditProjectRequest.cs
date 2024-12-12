using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Projects.Edit;

public sealed class EditProjectRequest(int projectId) : IRequest<Result>
{
    public int ProjectId { get; set; } = projectId;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;
}

public sealed class EditProjectValidator : AbstractValidator<EditProjectRequest>
{
    public EditProjectValidator()
    {
        this.RuleFor(x => x.ProjectId).GreaterThan(0);
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Description).MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Color).Matches(ValidationConstants.HexColorRegex).When(x => !string.IsNullOrWhiteSpace(x.Color));
    }
}
