using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.BL.ExternalService
{
    public class ReminderCheckService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReminderCheckService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TaskManagementDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var now = DateTime.Now;
                var reminders = await context.Reminders
                    .Include(r => r.Appointment)
                    .Where(r => r.Appointment!.StartDate.AddMinutes(-30) <= now && r.Appointment.StartDate > now)
                    .ToListAsync();

                foreach (var reminder in reminders)
                {
                    string emailBody = $@"
                       <div style=""font-family: Arial, sans-serif; line-height: 1.8; background-color: #f9f9f9; padding: 15px; border-radius: 8px;"">
                        <h3 style=""color: #2c3e50; font-size: 22px; font-weight: bold; margin-bottom: 15px; border-bottom: 2px solid #3498db; padding-bottom: 5px;"">
                            Görüş Detalları
                        </h3>
                        <p style=""margin: 10px 0; font-size: 18px; font-weight: bold; color: #333;"">
                            <strong>Təsvir:</strong> {{reminder.Appointment.DateName}}
                        </p>
                        <p style=""margin: 10px 0; font-size: 18px; font-weight: bold; color: #333;"">
                            <strong>Mesaj:</strong> {{reminder.Message}}
                        </p>
                    </div>";

                    emailService.SendEMail(reminder.Appointment.HostEmail!, "XATIRLATMA !", emailBody);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
