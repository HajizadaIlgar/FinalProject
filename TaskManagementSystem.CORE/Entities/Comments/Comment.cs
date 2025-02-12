using TaskManagementSystem.CORE.Entities.CommentLikes;
using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.CORE.Entities.Comments;

public class Comment : BaseEntity
{
    public string CommentDescription { get; set; }
    public string? ImagesUrl { get; set; }
    public AppUser? User { get; set; }
    public int UserId { get; set; }
    public ICollection<Like> Likes { get; set; }
}
