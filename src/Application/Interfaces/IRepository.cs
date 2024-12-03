using Ardalis.Specification;
using Etn.MyLittleBoard.Domain.Interfaces;

namespace Etn.MyLittleBoard.Application.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
