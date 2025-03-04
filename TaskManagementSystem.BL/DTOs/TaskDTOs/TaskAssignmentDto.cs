using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.BL.DTOs.TaskDTOs
{
    public class TaskAssignmentsDto
    {
        // Tapşırığa assign edilmiş istifadəçilərin siyahısı
        public List<AppUser> AppUsers { get; set; }

        // Tapşırıq haqqında məlumatlar (istəyə görə əlavə edə bilərsiniz)
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }

}
