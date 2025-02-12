using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.CORE.Entities.Notfications;

namespace TaskManagementSystem.CORE.Configurations.Notfications;

public class NotficationConfiguration : IEntityTypeConfiguration<Notfication>
{
    public void Configure(EntityTypeBuilder<Notfication> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(128);
        builder
            .HasOne(x => x.Task)
            .WithMany(x => x.Notfications)
            .HasForeignKey(x => x.TaskId);
    }
}
