using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Aggregate.Entities;
using Banking.Core.Services;
using Banking.Core.Utils;

namespace Banking.Web.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        protected readonly ITransactionRepository _repository;
        private TransactionService transactionService;
        public TransactionController(ITransactionRepository repository) 
        { 
            _repository = repository;
            transactionService = new TransactionService();
        }

        [HttpPost("Deposit")]
        public virtual async Task<ActionResult> Deposit([FromServices]IBankAccountRepository accountRepository,
                                                            TransactionDTO transactionDTO)
        {
            BankAccount? account = await accountRepository.Get(transactionDTO.destinyAccountId);
            Transaction transaction = transactionService.CreateTransaction(TransactionType.Deposit, transactionDTO.value, account);
            if (transaction == null) return BadRequest(TransactionMessageConstant.DepositError);
            account.Balance = transactionService.ExecuteDeposit(account,transactionDTO.value);
            int? id = await accountRepository.Update(account);
            if (id == null) { return UnprocessableEntity(MessageConstant.ItemNotUpdated(typeof(Transaction), id)); }
            int idTransaction = await _repository.Add(transaction);
            return Ok(new { message = MessageConstant.ItemCreated(typeof(Transaction),idTransaction),
                            id = idTransaction });
        }

        [HttpPost("Withdraw")]
        public virtual async Task<ActionResult> Withdraw([FromServices]IBankAccountRepository accountRepository,
                                                            TransactionDTO transactionDTO)
        {
            BankAccount? account = await accountRepository.Get(transactionDTO.destinyAccountId);
            Transaction transaction = transactionService.CreateTransaction(TransactionType.Withdraw, transactionDTO.value, account);
            if (transaction == null) return BadRequest(TransactionMessageConstant.WithdrawError);
            if (!transactionService.ValidateWithdraw(account,transactionDTO.value)) return BadRequest();
            account.Balance = transactionService.ExecuteWithdraw(account,transactionDTO.value);
            int? id = await accountRepository.Update(account);
            if (id == null) { return UnprocessableEntity(MessageConstant.ItemNotUpdated(typeof(Transaction), id)); }
            
            int idTransaction = await _repository.Add(transaction);
            return Ok(new { message = MessageConstant.ItemCreated(typeof(Transaction),idTransaction),
                            id = idTransaction });
        }

        [HttpPost("Transfer")]
        public virtual async Task<ActionResult> Transfer([FromServices]IBankAccountRepository accountRepository,
                                                            TransactionDTO transactionDTO)
        {
            BankAccount? originAccount = await accountRepository.Get(transactionDTO.originAccountId);
            BankAccount? destinyAccount = await accountRepository.Get(transactionDTO.destinyAccountId);
            Transaction transaction = transactionService.CreateTransaction(TransactionType.Transfer, transactionDTO.value, originAccount, destinyAccount);
            if (transaction == null) BadRequest(TransactionMessageConstant.TransferError);
            if (!transactionService.ValidateWithdraw(originAccount,transactionDTO.value)) return BadRequest();
            originAccount.Balance = transactionService.ExecuteWithdraw(originAccount,transactionDTO.value);
            destinyAccount.Balance = transactionService.ExecuteDeposit(destinyAccount,transactionDTO.value);
            int? originId = await accountRepository.Update(originAccount);
            if (originId == null) { return UnprocessableEntity(MessageConstant.ItemNotUpdated(typeof(Transaction), originId)); }
            int? destinyId = await accountRepository.Update(destinyAccount);
            if (destinyId == null) { return UnprocessableEntity(MessageConstant.ItemNotUpdated(typeof(Transaction), destinyId)); }
            
            int idTransaction = await _repository.Add(transaction);
            return Ok(new { message = MessageConstant.ItemCreated(typeof(Transaction),idTransaction),
                            id = idTransaction });
        }
    } 
}