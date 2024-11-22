using Ardalis.Result;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;

namespace Etn.MyLittleBoard.Application.Projects.ListPaginated;

public sealed record ListPaginatedProjectsRequest(int Skip, int Take) : IRequest<Result<Project[]>>;
