using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Tasks;

namespace TaskManagementSystem.CORE.Entities.Tags
{
    public class Tag : BaseEntity
    {
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
