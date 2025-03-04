using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.CORE.Entities.Tags;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.CORE.Entities.Tasks
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public TaskItemStatus Status { get; set; }
        public int StatusId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<AppUser> Users { get; set; }
        public string UsersId { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<Notfication>? Notfications { get; set; }
    }
}
