using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.CORE.Entities.Categories;
using TaskManagementSystem.CORE.Entities.CommentLikes;
using TaskManagementSystem.CORE.Entities.Comments;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.CORE.Entities.Plans;
using TaskManagementSystem.CORE.Entities.Tags;
using TaskManagementSystem.CORE.Entities.Tasks;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.DAL.Contexts;

public class TaskManagementDbContext : IdentityDbContext<AppUser>
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> opt) : base(opt) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<TaskItemStatus> TaskItemsStatus { get; set; }
    public DbSet<Notfication> Notifications { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DateReminder> Reminders { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
    }
}
