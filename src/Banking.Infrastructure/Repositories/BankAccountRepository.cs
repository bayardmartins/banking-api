using Microsoft.EntityFrameworkCore;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BankingContext dataContext) : base(dataContext) {}

        public override async Task<int?> Update(BankAccount bankAccount)
        {
            var bankAccountToUpdate = await dbSet.FindAsync(bankAccount.Id);
            if(bankAccountToUpdate == null) { return null; }
            bankAccountToUpdate.AccountNumber = bankAccount.AccountNumber;
            bankAccountToUpdate.Password = bankAccount.Password;
            bankAccountToUpdate.IsActive = bankAccount.IsActive;

            await _context.SaveChangesAsync();
            return bankAccountToUpdate.Id;
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountByCustomerId(int customerId)
        {
            return await dbSet.Where(a => a.IsActive)
                                .Where(a => a.Customer.Id == customerId)
                                .ToListAsync();
        }
    }
}