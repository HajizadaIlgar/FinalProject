using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.DatePlannerDTOs;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.CORE.Entities.Plans;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DateController(TaskManagementDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DateCreateDto dto)
        {
            Appointment appointment = new Appointment
            {
                DateName = dto.DateName,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Reminders = dto.Reminders,
                Host = dto.Host,
                Notes = dto.Notes
            };
            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public void AddReminderToAppointment(int appointmentId, DateTime timeBefore, string message)
        {
            var appointment = _context.Appointments
                .Include(a => a.Reminders) // Appointment ilə Reminder-ləri birlikdə yüklə
                .FirstOrDefault(a => a.Id == appointmentId);

            if (appointment != null)
            {
                var reminder = new DateReminder
                {
                    AppointmentId = appointmentId,
                    TimeBefore = timeBefore,
                    Message = message
                };
                appointment.Reminders.Add(reminder);
                _context.SaveChanges(); // Database-ə dəyişikliyi yadda saxla
            }
        }
        public List<DateReminder> GetRemindersForAppointment(int appointmentId)
        {
            return _context.Reminders
                .Where(r => r.AppointmentId == appointmentId)
                .ToList();
        }
        public void CheckAndSendReminders()
        {
            var now = DateTime.Now;

            var reminders = _context.Reminders
                .Include(r => r.Appointment)
                .Where(r => r.Appointment.StartDate.AddMinutes(-r.TimeBefore.Minute) <= now)
                .ToList();

            foreach (var reminder in reminders)
            {
                // Burada istifadəçiyə xəbərdarlıq göndərə bilərsən
                Console.WriteLine($"Reminder: {reminder.Message} - Görüş: {reminder.Appointment.DateName}");
            }
        }

    }
}
