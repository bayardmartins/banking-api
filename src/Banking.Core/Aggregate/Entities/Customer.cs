using System.ComponentModel.DataAnnotations;

namespace Banking.Core.Aggregate.Entities
{
    public class Customer : EntityBase
    {
        [Required]
        public string Name {get;set;}
        [Required]
        public string Password {get;set;}
        [Required]
        public string NuDocument {get;set;}
    }
}