using Xunit;
using Banking.Core.Services;
using Banking.Core.Aggregate.Entities;

namespace Banking.Tests.UnitTests.Core
{
    public class TransactionService_Tests
    {
        TransactionService transactionService = new TransactionService();
        
        [Fact]
        public void TransactionExecuteDeposit_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200;
            var result = transactionService.ExecuteDeposit(bankAccount,value);

            Assert.Equal(1200, result);
        }

        [Fact]
        public void TransactionValidateWithdraw_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200;
            var result = transactionService.ValidateWithdraw(bankAccount,value);

            Assert.Equal(true, result);
        }

        [Fact]
        public void TransactionValidateWithdraw_ReturnFalse()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 1200;
            var result = transactionService.ValidateWithdraw(bankAccount,value);

            Assert.Equal(false, result);
        }

        [Fact]
        public void TransactionExecuteWithdraw_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200;
            var result = transactionService.ExecuteWithdraw(bankAccount,value);

            Assert.Equal(800, result);
        }

        [Fact]
        public void TransactionCreateDeposit_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200;
            var result = transactionService.CreateTransaction(TransactionType.Deposit,value,bankAccount);

            Assert.NotEqual(null, result);
            Assert.Equal(value,result.Amount);
        }

        [Fact]
        public void TransactionCreateWithdraw_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200;
            var result = transactionService.CreateTransaction(TransactionType.Withdraw,value,bankAccount);

            Assert.NotEqual(null, result);
            Assert.Equal(value,result.Amount);
        }

        [Fact]
        public void TransactionCreateTransfer_ReturnTrue()
        {
            BankAccount destinyAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };BankAccount originAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };

            double value = 200;
            var result = transactionService.CreateTransaction(TransactionType.Transfer,value,destinyAccount,originAccount);

            Assert.NotEqual(null, result);
            Assert.Equal(value,result.Amount);
        }


        [Fact]
        public void TransactionValidateValidTransaction_ReturnTrue()
        {
            BankAccount destinyAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 200.00;
            var result = transactionService.ValidateTransaction(destinyAccount,value);

            Assert.Equal(true,result);
        }

        [Fact]
        public void TransactionValidateInvalidValue_ReturnFalse()
        {
            BankAccount destinyAccount = new BankAccount{
                AccountNumber = "1234",
                Balance = 1000.00,
                Customer = null,
            };
            double value = 0;
            var result = transactionService.ValidateTransaction(destinyAccount,value);

            Assert.Equal(false,result);
        }

        [Fact]
        public void TransactionValidateNullBankAccount_ReturnFalse()
        {
            BankAccount destinyAccount = null;
            double value = 0;
            var result = transactionService.ValidateTransaction(destinyAccount,value);

            Assert.Equal(false,result);
        }
    }
}