using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Tasks;

namespace TaskManagementSystem.CORE.Entities.Notfications
{
    public class Notfication : BaseEntity
    {
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public int TaskId { get; set; }
        public TaskItem Task { get; set; } = null!;
    }
}
