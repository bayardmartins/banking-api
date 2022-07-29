using Banking.Core.Aggregate.Entities;
namespace Banking.Core.Services
{
    public class TransactionService
    {
        public double ExecuteDeposit(BankAccount account, double value)
        {
            account.Balance += value;
            return account.Balance;
        }

        public bool ValidateWithdraw(BankAccount account, double value)
        {
            return account.Balance - value >=0;
        }

        public double ExecuteWithdraw(BankAccount account, double value)
        {
            account.Balance -= value;
            return account.Balance;
        }

        public Transaction? CreateTransaction(int type, double value, BankAccount? destinyAccount, BankAccount? originAccount = null){
            if (!this.ValidateTransaction(destinyAccount, value)) return null;
            Transaction transaction = new Transaction {
                TransactionType = type,
                Amount = value,
                Register = DateTime.UtcNow,
                DestinyAccount = destinyAccount
            };
            if (type == TransactionType.Transfer)
            {
                if (originAccount == null) return null;
                transaction.OriginAccount = originAccount;
            }
            return transaction;
        }

        public bool ValidateTransaction(BankAccount? destinyAccount, double value)
        {
            bool validAccount = destinyAccount != null;
            bool validAmount = value > 0;
            return validAccount && validAmount;
        }

    }
}