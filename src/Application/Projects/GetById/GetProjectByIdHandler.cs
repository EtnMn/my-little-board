using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Aggregates.Projects.Specifications;

namespace Etn.MyLittleBoard.Application.Projects.GetById;

public sealed class GetProjectByIdHandler(IRepository<Project> repository) :
    IRequestHandler<GetProjectByIdRequest, Result<Project>>
{
    public async Task<Result<Project>> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
    {
        ProjectByIdNoTracking specification = new(ProjectId.From(request.Id));
        Project? project = await repository.FirstOrDefaultAsync(specification, cancellationToken);
        if (project is not null)
        {
            return project;
        }
        else
        {
            return Result<Project>.NotFound();
        }
    }
}

