namespace Banking.Core.Aggregate.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}