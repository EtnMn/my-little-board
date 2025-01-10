namespace Etn.MyLittleBoard.Application.Clients.EditState;

public sealed record EditClientStateRequest(int ClientId, bool Enable) : IRequest<Result>;
