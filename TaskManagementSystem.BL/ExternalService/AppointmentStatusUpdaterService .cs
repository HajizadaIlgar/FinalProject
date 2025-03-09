using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Enums;
using TaskManagementSystem.DAL.Contexts;

public class AppointmentStatusUpdater : IAppointmentStatusUpdater
{
    private readonly TaskManagementDbContext _dbContext;
    private readonly ILogger<AppointmentStatusUpdater> _logger;

    public AppointmentStatusUpdater(TaskManagementDbContext dbContext, ILogger<AppointmentStatusUpdater> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task UpdateStatusesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var appointmentsToUpdate = await _dbContext.Appointments
            .Where(a => a.Status == DateStatus.Scheduled && a.EndDate < now)
            .ToListAsync(cancellationToken);

        foreach (var appointment in appointmentsToUpdate)
        {
            if (appointment.IsAttended)
            {
                // Əgər istifadəçi iştirak edibsə, görüş tamamlanmış sayılır
                //appointment.Status = DateStatus.Completed;
            }
            else
            {
                if (appointment.EndDate < now)
                {
                    // Əgər görüş artıq başa çatıb və iştirak edilməyibsə, avtomatik olaraq Canceled
                    appointment.Status = DateStatus.Finished;
                }
                else if (appointment.StartDate > now)
                {
                    // Görüş gələcəkdədirsə və hələ baş verməyibsə, Scheduled qalır
                    appointment.Status = DateStatus.Scheduled;
                }
            }
        }

        if (appointmentsToUpdate.Any())
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Updated {appointmentsToUpdate.Count} appointments at {now}.");
        }
        else
        {
            _logger.LogInformation("No appointments were updated.");
        }
    }
}
