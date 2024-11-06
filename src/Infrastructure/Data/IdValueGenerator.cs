using Etn.MyLittleBoard.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Reflection;

namespace Etn.MyLittleBoard.Infrastructure.Data;

internal sealed class IdValueGenerator<TContext, TEntityBase, TId> : ValueGenerator<TId>
    where TContext : DbContext
    where TEntityBase : EntityBase<TEntityBase, TId>
    where TId : IVogen<TId, int>
{
    private readonly PropertyInfo matchPropertyGetter;

    public IdValueGenerator()
    {
        List<PropertyInfo> matchingProperties = typeof(TContext).GetProperties().Where(p => p.GetGetMethod()?.IsPublic == true && p.PropertyType == typeof(DbSet<TEntityBase>)).ToList();

        if (matchingProperties.Count == 0)
        {
            throw new InvalidOperationException($"No properties found in the EFCore context for a DBSet of {nameof(TEntityBase)}");
        }

        if (matchingProperties.Count > 1)
        {
            throw new InvalidOperationException($"Multiple properties found in the EFCore context for a DBSet of {nameof(TEntityBase)}");
        }

        this.matchPropertyGetter = matchingProperties[0];
    }

    public override TId Next(EntityEntry entry)
    {
        TContext ctx = (TContext)entry.Context;

        DbSet<TEntityBase>? entities = (DbSet<TEntityBase>?)this.matchPropertyGetter.GetValue(ctx) ?? throw new InvalidOperationException($"DbSet not found in the EFCore context for {nameof(TEntityBase)}");

        int next = Math.Max(MaxFrom(entities.Local), MaxFrom(entities)) + 1;
        return TId.From(next);

        static int MaxFrom(IEnumerable<TEntityBase> es)
        {
            return es.Any() ? es.Max(e => e.Id.Value) : 0;
        }
    }

    public override bool GeneratesTemporaryValues => false;
}
