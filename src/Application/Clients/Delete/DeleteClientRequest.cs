namespace Etn.MyLittleBoard.Application.Clients.Delete;

public sealed record DeleteClientRequest(int ClientId) : IRequest<Result>;
