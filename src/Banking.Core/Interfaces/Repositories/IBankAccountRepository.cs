using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Interfaces.Repositories
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount>
    {
        Task<IEnumerable<BankAccount>> GetBankAccountByCustomerId(int customerId);
    }
}