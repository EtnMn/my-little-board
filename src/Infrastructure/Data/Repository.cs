using Ardalis.Specification.EntityFrameworkCore;
using Etn.MyLittleBoard.Domain.Interfaces;

namespace Etn.MyLittleBoard.Infrastructure.Data;

internal sealed class Repository<T>(AppDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T> where T : class, IAggregateRoot
{
}
