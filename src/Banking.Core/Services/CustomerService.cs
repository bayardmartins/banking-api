using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Services
{
    public class CustomerService
    {
        public bool ValidateCustomer(Customer customer)
        {
            bool isValidPassword = !customer.Password.Contains(' ') && customer.Password.Length < 30 && customer.Password.Length > 4;
            return isValidPassword;
        }
    }
}