using Etn.MyLittleBoard.Domain.Aggregates;
using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using Etn.MyLittleBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Etn.MyLittleBoard.Infrastructure.Data;

internal sealed class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher) :
    DbContext(options)
//IAppDbContext
{
    private readonly IDomainEventDispatcher? dispatcher = dispatcher;

    public DbSet<Project> Projects => this.Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        if (this.dispatcher is null)
        {
            return result;
        }

        HasDomainEventsBase[] entitiesWithEvents = this.ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count > 0)
            .ToArray();

        await this.dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }
}
