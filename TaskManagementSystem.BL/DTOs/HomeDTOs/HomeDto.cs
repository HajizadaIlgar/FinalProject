using TaskManagementSystem.CORE.Entities.Plans;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.BL.DTOs.HomeDTOs
{
    public class HomeDto
    {
        public List<AppUser> AppUsers { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
