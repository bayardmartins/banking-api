using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T item);
        Task<int?> Delete(int id);
        Task<int?> Update(T item);
        Task<int?> SetIsActivate(int id, bool isActive);
    }
}