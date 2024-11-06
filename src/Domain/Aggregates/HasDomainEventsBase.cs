using Etn.MyLittleBoard.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etn.MyLittleBoard.Domain.Aggregates;

public abstract class HasDomainEventsBase : IHasDomainEvents
{
    private readonly List<DomainEventBase> domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => this.domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        this.domainEvents.Clear();
    }

    protected void RegisterDomainEvent(DomainEventBase domainEvent)
    {
        this.domainEvents.Add(domainEvent);
    }
}
