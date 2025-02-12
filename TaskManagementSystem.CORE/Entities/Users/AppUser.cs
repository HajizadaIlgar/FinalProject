using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.CORE.Entities.CommentLikes;
using TaskManagementSystem.CORE.Entities.Comments;
using TaskManagementSystem.CORE.Entities.Tasks;

namespace TaskManagementSystem.CORE.Entities.Users
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImagerlUrl { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
    }
}
