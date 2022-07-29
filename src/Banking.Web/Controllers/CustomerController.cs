using Microsoft.AspNetCore.Mvc;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Core.Services;
using Banking.Core.Utils;

namespace Banking.Web.Controllers
{
    public class CustomerController : BaseController<Customer>
    {
        private CustomerService customerService;
        public CustomerController(ICustomerRepository repository) : base(repository) { customerService = new CustomerService(); }

        [HttpPost("{id}/Inactivate")]
        public async Task<ActionResult> InactivateCustomer([FromServices]IBankAccountRepository accountRepository,
                                                            int id)
        {
            var result = await GetItemById(id);
            if(result == null) return NotFound(MessageConstant.ItemNotFound(typeof(Customer)));
            Customer customer = result.Value;
            var accountList = await accountRepository.GetBankAccountByCustomerId(id);
            if(accountList != null && accountList.Count() > 0) return UnprocessableEntity(CustomerMessageConstant.CustomerWithAccounts(id));
            int? inactivatedId = await _repository.SetIsActivate(id,false);
            return Ok(new { message = MessageConstant.ItemDeleted(typeof(Customer),inactivatedId),
                            id = inactivatedId });
        }

        [HttpPost("{id}/Activate")]
        public async Task<ActionResult> ActivateCustomer([FromServices]IBankAccountRepository accountRepository,
                                                            int id)
        {
            try
            {
                int? inactivatedId = await _repository.SetIsActivate(id,true);
                return Ok(new { message = MessageConstant.ItemDeleted(typeof(Customer),inactivatedId),
                                id = inactivatedId });
            }
            catch
            {
                return NotFound(MessageConstant.ItemNotFound(typeof(Customer)));
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                if (!customerService.ValidateCustomer(customer)) { return UnprocessableEntity(CustomerMessageConstant.InvalidCustomerData); }
                int id = await _repository.Add(customer);
                return Ok(new { message = MessageConstant.ItemCreated(typeof(Customer),id),
                                id = id });
            } 
            catch 
            {
                return UnprocessableEntity(MessageConstant.ItemNotCreated(typeof(Customer)));
            }
        }

        public override async Task<ActionResult> UpdateItem(int id, Customer customer)
        {
            if (!customerService.ValidateCustomer(customer)) { return UnprocessableEntity(CustomerMessageConstant.InvalidCustomerData); }
            return await base.UpdateItem(id,customer);
        }
    }
}