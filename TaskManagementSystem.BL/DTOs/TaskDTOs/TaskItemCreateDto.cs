using TaskManagementSystem.CORE.Entities.Tasks;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.BL.DTOs.TaskDTOs
{
    public class TaskItemCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public TaskItemStatus Status { get; set; }
        public int StatusId { get; set; }
        public ICollection<AppUser> Users { get; set; }
        public string UserId { get; set; }
    }
}
