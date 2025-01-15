using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.Application.Clients.GetById;

public sealed record GetClientByIdRequest(int Id) : ICachedRequest<Result<Client>>
{
    public string Key => $"client-{this.Id}";

    public string[]? Tags => ["clients"];
}
