using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.CORE.Entities.Comments;

namespace TaskManagementSystem.CORE.Configurations.Comments;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.CommentDescription)
            .IsRequired()
            .HasMaxLength(528);
        builder
            .Property(e => e.ImagesUrl)
            .IsRequired()
            .HasMaxLength(256);
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.UserId);
    }
}
