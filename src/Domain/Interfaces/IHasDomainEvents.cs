using Etn.MyLittleBoard.Domain.Aggregates;

namespace Etn.MyLittleBoard.Domain.Interfaces;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
}
