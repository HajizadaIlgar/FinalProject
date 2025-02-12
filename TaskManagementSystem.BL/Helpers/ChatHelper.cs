using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.BL.Helpers
{
    public class ChatHelper
    {
        public static ICollection<AppUser> Users { get; set; } = new HashSet<AppUser>();
    }
}
