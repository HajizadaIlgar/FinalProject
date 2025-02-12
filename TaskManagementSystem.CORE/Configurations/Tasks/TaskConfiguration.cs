using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.CORE.Entities.Tasks;

namespace TaskManagementSystem.CORE.Configurations.Tasks;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(32);
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(128);
        builder
            .HasMany(x => x.Users)
            .WithMany(x => x.Tasks);
        builder
            .HasOne(x => x.Status)
            .WithMany(x => x.TaskItems)
            .HasForeignKey(x => x.StatusId);
        builder
         .HasMany(e => e.Tags)
        .WithMany(e => e.Tasks);
    }
}
