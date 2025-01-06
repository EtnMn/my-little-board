using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.Application.Clients.GetById;

public sealed record GetClientByIdRequest(int Id) : IRequest<Result<Client>>;
