using System.ComponentModel.DataAnnotations;

namespace Banking.Core.Aggregate.Entities
{
    public class BankAccount : EntityBase
    {
        [Required]
        public string AccountNumber {get;set;}
        [Required]
        public string Password {get;set;}
        public double Balance {get;set;} = 0;
        public int CustomerId {get;set;}
        public virtual Customer? Customer {get;set;}
    }
}