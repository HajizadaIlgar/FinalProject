using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.CORE.Entities.Plans;

namespace TaskManagementSystem.CORE.Configurations.Appointments
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
               .Property(a => a.DateName)
               .IsRequired()
               .HasMaxLength(200);

            builder
                .Property(a => a.Description)
                .HasMaxLength(500);

            builder
                .Property(a => a.Location)
                .HasMaxLength(200);

            builder
                .Property(a => a.Host)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Notes)
                .HasMaxLength(1000);

            builder.Property(a => a.StartDate)
                .IsRequired();

            builder
                .Property(a => a.EndDate)
                .IsRequired();
            builder
                .HasOne(x => x.Reminders)
                .WithMany()
                .HasForeignKey("AppointmentId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
