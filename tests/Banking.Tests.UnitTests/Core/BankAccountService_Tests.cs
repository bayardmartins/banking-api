using Xunit;
using Banking.Core.Services;
using Banking.Core.Aggregate.Entities;

namespace Banking.Tests.UnitTests.Core
{
    public class BankAccountService_Tests
    {
        BankAccountService bankAccountService = new BankAccountService();

        [Fact]
        public void BankAccountCustomerNotFound_ReturnNull()
        {
            BankAccount bankAccount = new BankAccount(); 
            bankAccount.AccountNumber = "1234";
            bankAccount.Balance = 1000.00;
            Customer customer = null;
            
            var result = bankAccountService.CreateBankAccount(customer,bankAccount);

            Assert.Equal(null, result);
        }

        [Fact]
        public void BankAccountCustomerFound_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount(); 
            bankAccount.AccountNumber = "1234";
            bankAccount.Balance = 1000.00;
            Customer customer = new Customer();
            customer.Name = "Name Test";
            customer.Password = "pass";
            customer.NuDocument = "123456";
            
            var result = bankAccountService.CreateBankAccount(customer,bankAccount);
            bankAccount.Customer = customer;
            
            Assert.Equal(bankAccount, result);
        }

        [Fact]
        public void BankAccountValidateDelete_ReturnFalse()
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.AccountNumber = "1234";
            bankAccount.Balance = 123;
            var result = bankAccountService.ValidateAccountToDelete(bankAccount);

            Assert.Equal(false, result);
        }

        [Fact]
        public void BankAccountValidateDelete_ReturnTrue()
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.AccountNumber = "1234";
            bankAccount.Balance = 0;
            var result = bankAccountService.ValidateAccountToDelete(bankAccount);

            Assert.Equal(true, result);
        }

        [Fact]
        public void BankAccountValidadeMoth_ReturnTrue()
        {
            int month = 10;
            var result = bankAccountService.ValidateStatementRequest(month);

            Assert.Equal(true,result);
        }

        [Fact]
        public void BankAccountValidadeMoth_ReturnFalse()
        {
            int month = 13;
            var result = bankAccountService.ValidateStatementRequest(month);

            Assert.Equal(false,result);
        }
    }
}