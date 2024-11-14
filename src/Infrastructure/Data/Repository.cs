using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Interfaces;

namespace Etn.MyLittleBoard.Infrastructure.Data;

internal sealed class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : class, IAggregateRoot
{
    private readonly AppDbContext dbContext = dbContext;

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        this.dbContext.Set<T>().Add(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public Task<int> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        this.dbContext.Set<T>().Remove(entity);
        return this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public ValueTask<T?> GetByIdAsync<TId>(TId entityId, CancellationToken cancellationToken) where TId : notnull
    {
        return this.dbContext.Set<T>().FindAsync([entityId], cancellationToken: cancellationToken);
    }

    public Task<int> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        this.dbContext.Set<T>().Update(entity);
        return this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
