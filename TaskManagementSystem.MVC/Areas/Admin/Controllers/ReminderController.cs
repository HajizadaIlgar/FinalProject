using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.ReminderDTOs;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReminderController(TaskManagementDbContext _context, IEmailService _emailService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reminders.ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            var formattedReminders = await _context.Appointments
                .Select(r => new
                {
                    Id = r.Id,
                    DisplayText = $"Appointment:{r.DateName} --- StartDate: {r.StartDate:yyyy-MM-dd HH:mm}"
                }).ToListAsync();
            ViewBag.ReminderOptions = formattedReminders;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DateReminderDto dto)
        {
            ViewBag.ReminderOptions = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();

            var appoint = await _context.Appointments.Where(x => x.Id == dto.AppointmentId).ToListAsync();
            if (appoint == null)
            {
                return BadRequest("Appointment not found.");
            }
            DateReminder reminder = new DateReminder
            {
                AppointmentId = dto.AppointmentId,
                TimeBefore = dto.TimeBefore,
                Message = dto.Message,
            };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Reminders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null) return BadRequest();
            _context.Reminders.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task CheckAndSendReminders()
        {
            var now = DateTime.Now;
            var reminders = await _context.Reminders
                 .Include(r => r.Appointment)
                 .Where(r => r.Appointment!.StartDate.AddMinutes(-30) <= now && r.Appointment.StartDate > now)
                 .ToListAsync();
            foreach (var reminder in reminders)
            {
                if (reminder.Appointment == null) continue;

                string emailBody = $@"
                            <h3>Görüş Detalları</h3>
                            <p>Təsvir: {reminder.Appointment.DateName}</p>
                            <p>Mesaj: {reminder.Message}</p>";

                _emailService.SendEMail(reminder.Appointment.HostEmail!, "Xatirlatma", emailBody);
            }
        }
    }
}
