using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Tasks;

namespace TaskManagementSystem.CORE.Entities.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TaskItem>? TaskItems { get; set; }
    }
}
