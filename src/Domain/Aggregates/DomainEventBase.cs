using MediatR;

namespace Etn.MyLittleBoard.Domain.Aggregates;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}