using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.CORE.Entities.Tasks
{
    public class TaskAssignment
    {
        public int Id { get; set; } 
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public ICollection<AppUser> User { get; set; }
        public int AssignedByAdminId { get; set; }  // Tapşırığı verən adminin ID-si
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
