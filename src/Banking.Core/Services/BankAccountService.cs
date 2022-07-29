using Banking.Core.Aggregate.Entities;
using Banking.Core.Interfaces.Repositories;

namespace Banking.Core.Services
{
    public class BankAccountService
    {
        public BankAccount? CreateBankAccount(Customer? customer, BankAccount bankAccount)
        {
            if(customer == null) return null;    
            bankAccount.Customer = customer;
            return bankAccount;
        }

        public bool ValidateAccountToDelete(BankAccount? bankAccount)
        {
            return bankAccount.Balance == 0;
        }

        public bool ValidateStatementRequest(int month)
        {
            return month > 0 && month <= 12;
        }
    }
}