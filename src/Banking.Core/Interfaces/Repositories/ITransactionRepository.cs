using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<List<Transaction>> GetTransactionsByAccountAndMonth(int id, DateTime initialDate, DateTime finalDate);
    }
}