using Vogen;

[assembly: VogenDefaults(staticAbstractsGeneration: StaticAbstractsGeneration.MostCommon | StaticAbstractsGeneration.InstanceMethodsAndProperties)]

namespace Etn.MyLittleBoard.Domain.Aggregates;

public abstract class EntityBase<T, TId> : HasDomainEventsBase
  where T : EntityBase<T, TId>
{
    public TId Id { get; set; } = default!;
}
