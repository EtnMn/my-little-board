using Etn.MyLittleBoard.Domain.Aggregates;
using Etn.MyLittleBoard.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Etn.MyLittleBoard.Infrastructure;

public sealed class DomainEventDispatcher(
    IPublisher publisher,
    ILogger<DomainEventDispatcher> logger) :
    IDomainEventDispatcher
{
    private readonly IPublisher publisher = publisher;
    private readonly ILogger<DomainEventDispatcher> logger = logger;

    public async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (IHasDomainEvents entity in entitiesWithEvents)
        {
            if (entity is HasDomainEventsBase hasDomainEvents)
            {
                DomainEventBase[] events = [.. hasDomainEvents.DomainEvents];
                hasDomainEvents.ClearDomainEvents();

                foreach (DomainEventBase domainEvent in events)
                {
                    await this.publisher.Publish(domainEvent).ConfigureAwait(false);
                }
            }
            else
            {
                this.logger.LogError(
                    "Entity of type {EntityType} does not inherit from {BaseType}. Unable to clear domain events.",
                    entity.GetType().Name,
                    nameof(HasDomainEventsBase));
            }
        }
    }
}
