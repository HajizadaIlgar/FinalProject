namespace TaskManagementSystem.CORE.Entities.Commons
{
    public class BaseEntity : AuditableEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
