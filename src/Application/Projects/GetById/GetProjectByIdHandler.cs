using Ardalis.Result;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.GetById;

public sealed class GetProjectByIdHandler(IRepository<Project> repository) :
    IRequestHandler<GetProjectByIdRequest, Result<Project>>
{
    public async Task<Result<Project>> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
    {
        Project? project = await repository.GetByIdAsync(ProjectId.From(request.Id), cancellationToken);
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

