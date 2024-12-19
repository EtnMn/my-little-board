using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.EditStatus;

public sealed record EditProjectStatusRequest(int ProjectId, ProjectStatus ProjectStatus) : IRequest<Result>;

public sealed class EditProjectStatusValidator : AbstractValidator<EditProjectStatusRequest>
{
    public EditProjectStatusValidator()
    {
        this.RuleFor(x => x.ProjectId).GreaterThan(0);
        this.RuleFor(x => x.ProjectStatus).IsInEnum();
    }
}
