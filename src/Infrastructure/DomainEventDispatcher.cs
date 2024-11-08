using Etn.MyLittleBoard.Domain.Aggregates;
using Etn.MyLittleBoard.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Etn.MyLittleBoard.Infrastructure;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator mediator;
    private readonly ILogger<DomainEventDispatcher> logger;

    public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

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
                    await this.mediator.Publish(domainEvent).ConfigureAwait(false);
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
