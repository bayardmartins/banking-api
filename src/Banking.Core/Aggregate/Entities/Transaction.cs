using System.ComponentModel.DataAnnotations;

namespace Banking.Core.Aggregate.Entities
{
    public class Transaction : EntityBase
    {
        [Required]
        public int TransactionType {get;set;}
        [Required]
        public double Amount {get;set;}
        [Required]
        public DateTime Register {get;set;}
        public int? OriginAccountId {get;set;}
        public BankAccount? OriginAccount {get;set;}
        public int DestinyAccountId {get;set;}
        [Required]
        public BankAccount DestinyAccount {get;set;}
    }

    public static class TransactionType
    {
        public const int Deposit = 1;
        public const int Withdraw = 2;
        public const int Transfer = 3;
    }

    public class TransactionDTO
    {
        public int originAccountId {get;set;}
        public int destinyAccountId {get;set;}
        public double value {get;set;}
    }
}