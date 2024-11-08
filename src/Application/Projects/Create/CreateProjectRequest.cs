using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using MediatR;

namespace Etn.MyLittleBoard.Application.Projects.Create;

public record CreateProjectRequest(string Name) : IRequest<Result<ProjectId>>;
