namespace Etn.MyLittleBoard.Domain.Aggregates.Clients.Events;

public sealed class ClientDeletedEvent(ClientId clientId) : DomainEventBase
{
    public int ClientId { get; } = clientId.Value;
}
