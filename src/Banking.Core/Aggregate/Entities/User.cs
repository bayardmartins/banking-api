using System.ComponentModel.DataAnnotations;

namespace Banking.Core.Aggregate.Entities
{
    public class User : EntityBase
    {
        [Required]
        public string Login {get;set;}
        [Required]
        public string Password {get;set;}
    }
}