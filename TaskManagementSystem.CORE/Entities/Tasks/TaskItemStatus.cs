using TaskManagementSystem.CORE.Entities.Commons;

namespace TaskManagementSystem.CORE.Entities.Tasks;

public class TaskItemStatus : BaseEntity
{
    public string TaskStatus { get; set; }
    public ICollection<TaskItem>? TaskItems { get; set; }
}
