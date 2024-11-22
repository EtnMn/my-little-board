using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Constants;

namespace Etn.MyLittleBoard.Application.Projects.Create;

public sealed record CreateProjectRequest(string Name) : IRequest<Result<ProjectId>>;

public sealed class CreateProjectValidator
    : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.DefaultNameLength);
    }
}
