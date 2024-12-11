using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Projects.Create;

public sealed class CreateProjectRequest(string name, string? description) : IRequest<Result<ProjectId>>
{
    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;
}

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultTextLength);
        this.RuleFor(x => x.Description).MaximumLength(ValidationConstants.DefaultTextLength);
    }
}
