using TaskManagementSystem.CORE.Entities.Comments;
using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.CORE.Entities.CommentLikes;
public class Like : BaseEntity
{
    public int? LikeCount { get; set; }
    public int CommentId { get; set; }
    public Comment? Comment { get; set; }
    public int UserId { get; set; }
    public AppUser? User { get; set; }
}
