using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.GetById;

public sealed record GetProjectByIdRequest(int Id) : IRequest<Result<Project>>;

