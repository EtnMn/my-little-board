namespace Etn.MyLittleBoard.Domain.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}
