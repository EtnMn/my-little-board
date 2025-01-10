namespace Etn.MyLittleBoard.Application.Clients.CountActive;

public sealed record CountActiveClientsRequest() : IRequest<Result<int>>;
