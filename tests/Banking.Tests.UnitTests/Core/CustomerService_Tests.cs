using Xunit;
using Banking.Core.Services;
using Banking.Core.Aggregate.Entities;

namespace Banking.Tests.UnitTests.Core
{
    public class CustomerService_Tests
    {
        CustomerService customerService = new CustomerService();

        [Fact]
        public void CustomerValidateCustomer_ReturnTrue()
        {
            Customer customer = new Customer{
                Name = "Name Test",
                Password = "strongPassword",
                NuDocument = "12345-10"
            };
            var result = customerService.ValidateCustomer(customer);
        
            Assert.Equal(true,result);
        }

        [Fact]
        public void CustomerValidateCustomerPasswordToBig_ReturnFalse()
        {
            Customer customer = new Customer{
                Name = "Name Test",
                Password = "strongPasswordstrongPasswordstrongPasswordstrongPassword",
                NuDocument = "12345-10"
            };
            var result = customerService.ValidateCustomer(customer);
        
            Assert.Equal(false,result);
        }

        [Fact]
        public void CustomerValidateCustomerPasswordTooSmall_ReturnSmall()
        {
            Customer customer = new Customer{
                Name = "Name Test",
                Password = "pass",
                NuDocument = "12345-10"
            };
            var result = customerService.ValidateCustomer(customer);
        
            Assert.Equal(false,result);
        }
    }
}