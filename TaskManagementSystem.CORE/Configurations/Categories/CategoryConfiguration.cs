using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.CORE.Entities.Categories;

namespace TaskManagementSystem.CORE.Configurations.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValue("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}
