using Microsoft.EntityFrameworkCore;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankingContext dataContext) : base(dataContext) {} 

        public async Task<List<Transaction>> GetTransactionsByAccountAndMonth(int id, DateTime initialDate, DateTime finalDate)
        {
            var transactionList = await dbSet.Where(tran => tran.DestinyAccountId == id)
                                            .Where(tran => tran.Register >= initialDate.ToUniversalTime())
                                            .Where(tran => tran.Register <= finalDate.ToUniversalTime())
                                            .Where(tran => tran.IsActive)
                                            .ToListAsync();
            
            return transactionList;
        }
    }
}