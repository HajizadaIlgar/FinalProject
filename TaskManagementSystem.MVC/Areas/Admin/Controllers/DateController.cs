using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.DatePlannerDTOs;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Entities.Plans;
using TaskManagementSystem.CORE.Enums;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DateController(TaskManagementDbContext _context, IEmailService _emailService, IAppointmentStatusUpdater _statusUpdater) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.Appointments.ToListAsync();
            return View(data);
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
                Host = dto.Host,
                Notes = dto.Notes,
                Status = DateStatus.Scheduled,
                HostEmail = dto.HostEmail,
                IsAttended = false
            };

            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();

            string emailBody = $@"
            <h3>Görüş Detalları</h3>
            <p>Görüşün Adı: {dto.DateName}</p>
            <p>Təsvir: {dto.Description}</p>
            <p>Başlama tarixi: {dto.StartDate}</p>
            <p>Yer: {dto.Location}</p>
            <p>Təşkilatçı: {dto.Host}</p>";

            _emailService.SendEMail(dto.HostEmail!, "Yeni Görüş Təyin Edildi", emailBody);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Appointments.Where(x => x.Id == id).Select(x => new DateCreateDto
            {
                DateName = x.DateName,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Location = x.Location,
                Host = x.Host,
                HostEmail = x.HostEmail,
                Notes = x.Notes,
            }).FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, DateCreateDto dto)
        {
            var data = await _context.Appointments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (dto.DateName is not null)
            {
                data.DateName = dto.DateName;
                data.Description = dto.Description;
                data.StartDate = dto.StartDate;
                data.EndDate = dto.EndDate;
                data.Location = dto.Location;
                data.CreatedAt = DateTime.Now;
                data.Host = dto.Host;
                data.HostEmail = dto.HostEmail;
                data.Notes = dto.Notes;
                data.IsAttended = false;
                await _context.SaveChangesAsync();
            }
            string emailBody = $@"
            <h3>Görüş Detalları</h3>
            <p>Görüşün Adı: {dto.DateName}</p>
            <p>Təsvir: {dto.Description}</p>
            <p>Başlama tarixi: {dto.StartDate}</p>
            <p>Yer: {dto.Location}</p>
            <p>Təşkilatçı: {dto.Host}</p>";

            _emailService.SendEMail(dto.HostEmail!, " Görüş Yenilendi ", emailBody);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Appointments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null) return BadRequest();
            _context.Appointments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManualStatusUpdate()
        {
            // Manual olaraq status yeniləməsini ucun.....
            await _statusUpdater.UpdateStatusesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
