using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankingContext dataContext) : base(dataContext) {}    

        public override async Task<int?> Update(Customer customer)
        {
            var customerToUpdate = await dbSet.FindAsync(customer.Id);
            if(customerToUpdate == null) { return null; }
            customerToUpdate.Name = customer.Name;
            customerToUpdate.Password = customer.Password;
            customerToUpdate.NuDocument = customer.NuDocument;
            customerToUpdate.IsActive = customer.IsActive;
            
            await _context.SaveChangesAsync();

            return customerToUpdate.Id;
        }   
    }
}