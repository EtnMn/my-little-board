using Ardalis.Specification;
using Etn.MyLittleBoard.Domain.Interfaces;

namespace Etn.MyLittleBoard.Application.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken);

    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken);

    ValueTask<T?> GetByIdAsync<TId>(TId entityId, CancellationToken cancellationToken) where TId : notnull;

    Task<T[]> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);
}
