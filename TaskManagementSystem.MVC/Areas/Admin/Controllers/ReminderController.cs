using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.ReminderDTOs;
using TaskManagementSystem.CORE.Entities.Notfications;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReminderController(TaskManagementDbContext _context) : Controller
    {
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateReminderDto dto)
        {
            var appoint = await _context.Appointments.Where(x => x.Id == dto.AppointmentId).ToListAsync();
            if (appoint == null)
            {
                return BadRequest("Appointment not found.");
            }

            DateReminder reminder = new DateReminder
            {
                AppointmentId = 1,
                TimeBefore = dto.TimeBefore,
                Message = dto.Message,
            };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
            return Ok("yaask");
            //return RedirectToAction("Index", "Appointment");
        }
    }

}
