using Microsoft.AspNetCore.Mvc;
using Banking.Core.Aggregate.Entities;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Services;
using Banking.Core.Utils;

namespace Banking.Web.Controllers
{
    public class BankAccountController : BaseController<BankAccount>
    {
        private BankAccountService bankAccountService;
        public BankAccountController(IBankAccountRepository repository) : base(repository) { bankAccountService = new BankAccountService();}    
    
        public override async Task<ActionResult> DeleteItem(int id)
        {
            var result = await GetItemById(id);
            if(result.Value == null) return NotFound(MessageConstant.ItemNotFound(typeof(BankAccount)));
            BankAccount account = result.Value;
            bool readyToDelete = this.bankAccountService.ValidateAccountToDelete(account);
            if (!readyToDelete) return BadRequest(BankAccountMessageConstant.AccountWithFoundsError);
            return await base.DeleteItem(id);
        }
        
        [HttpPost("{id}")]
        public async Task<ActionResult> CreateBankAccount([FromServices]ICustomerRepository customerRepository,
                                                            BankAccount bankAccount,
                                                            int id)
        {
            try
            {
                Customer? customer = await customerRepository.Get(id);
                BankAccount? bankAccountToAdd = this.bankAccountService.CreateBankAccount(customer,bankAccount);
                if(bankAccountToAdd == null) return UnprocessableEntity(MessageConstant.ItemNotCreated(typeof(BankAccount)));
                int newAccountId = await _repository.Add(bankAccountToAdd);
                
                return Ok(new { id = newAccountId,
                                message = MessageConstant.ItemCreated(typeof(BankAccount),newAccountId)
                });
            } 
            catch 
            {
                return UnprocessableEntity(MessageConstant.ItemNotCreated(typeof(BankAccount)));
            }
        }

        [HttpGet("{id}/Balance")]
        public async Task<ActionResult> GetBalanceByBankAccountId(int id)
        {
            var account = await _repository.Get(id);
            if (account == null) return NotFound();
            return Ok(account.Balance);
        }

        [HttpGet("{id}/Statement/{month}")]
        public async Task<ActionResult> GetStatement([FromServices]ITransactionRepository transactionRepository,
                                                        int id, int month)
        {
            if (!bankAccountService.ValidateStatementRequest(month)) { return BadRequest(BankAccountMessageConstant.InvalidMonth); }
            int year = DateTime.Now.Year;
            DateTime initialDate = new DateTime(year,month,1);
            DateTime finalDate = new DateTime(year,month,DateTime.DaysInMonth(year,month));
            var transactionList = await transactionRepository.GetTransactionsByAccountAndMonth(id,initialDate,finalDate);
            return Ok(transactionList);
        }

        [HttpGet("Customer/{id}")]
        public async Task<IEnumerable<BankAccount>> GetBankAccountListByCustomerId(int id)
        {
            var accountList = await (_repository as IBankAccountRepository).GetBankAccountByCustomerId(id);
            return accountList;
        }

        [HttpPost("{id}/Inactivate")]
        public async Task<ActionResult> InactivateBankAccount(int id)
        {
            var result = await GetItemById(id);
            if(result == null) return NotFound(MessageConstant.ItemNotFound(typeof(BankAccount)));
            BankAccount bankAccount = result.Value;
            int? inactivatedId = await _repository.SetIsActivate(id,false);
            return Ok(new { message = MessageConstant.ItemDeleted(typeof(BankAccount),inactivatedId),
                            id = inactivatedId
            });
        }
        [HttpPost("{id}/Activate")]
        public async Task<ActionResult> ActivateBankAccount(int id)
        {
            try{
                int? inactivatedId = await _repository.SetIsActivate(id,true);
                return Ok(new { message = MessageConstant.ItemActivated(typeof(BankAccount),inactivatedId),
                                id = inactivatedId
                });
            } catch 
            {
                return NotFound(MessageConstant.ItemNotFound(typeof(BankAccount)));
            }
        }
    }
}