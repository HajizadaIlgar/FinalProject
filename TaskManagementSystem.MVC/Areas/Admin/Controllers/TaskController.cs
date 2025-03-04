using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.TaskDTOs;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Entities.Tasks;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TaskController(TaskManagementDbContext _context, UserManager<AppUser> _userManager, IEmailService _emailService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.TaskItems
                .Include(t => t.Status)
                .Include(t => t.TaskAssignments)
                .Include(x => x.Users)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.TaskStatus = await _context.TaskItemsStatus.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string userId, TaskItemCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }
            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.TaskStatus = await _context.TaskItemsStatus.ToListAsync();
            TaskItem tasks = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DeadLine = dto.DeadLine,
                CreatedAt = DateTime.Now,
                StatusId = dto.StatusId,
                UsersId = dto.UserId
            };
            var users = _context.Users.Where(u => dto.UserId.Contains(u.Id)).ToList();
            tasks.Users = users;

            if (dto.UserId != null)
            {
                var taskAssignment = new TaskAssignment
                {
                    TaskId = tasks.Id,  // Yaradılan tapşırığın Id-si
                    UserId = dto.UserId  // Təyin edilmiş istifadəçi
                };
                await _context.TaskAssignments.AddAsync(taskAssignment);
                await _context.SaveChangesAsync();
            }

            await _context.TaskItems.AddAsync(tasks);
            await _context.SaveChangesAsync();

            string emailBody = $@"
            <h3> Detalları</h3>
            <p> Adı: {dto.Title}</p>
            <p>Edilecekler: {dto.Description}</p>
            <p>Bitme Vaxti: {dto.DeadLine}</p>";

            _emailService.SendEMail(user.Email!, "Yeni Task Teyin Edildi", emailBody);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.TaskStatus = await _context.TaskItemsStatus.ToListAsync();

            var data = await _context.TaskItems.Where(x => x.Id == id).Select(x => new TaskItemCreateDto
            {
                Title = x.Title,
                Description = x.Description,
                DeadLine = x.DeadLine,
                StatusId = x.StatusId,
                UserId = x.UsersId
            }).FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string userId, int? id, TaskItemCreateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.TaskStatus = await _context.TaskItemsStatus.ToListAsync();

            if (id is null) return BadRequest();
            var data = await _context.TaskItems.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            if (dto.Title is not null)
            {
                data.Title = dto.Title;
                data.Description = dto.Description;
                data.DeadLine = dto.DeadLine;
                data.StatusId = dto.StatusId;
                data.UsersId = dto.UserId;
                await _context.SaveChangesAsync();
            }
            string emailBody = $@"
            <h3> Detalları</h3>
            <p> Adı: {dto.Title}</p>
            <p>Edilecekler: {dto.Description}</p>
            <p>Bitme Vaxti: {dto.DeadLine}</p>";

            _emailService.SendEMail(user.Email!, "Task Yeniləndi", emailBody);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.TaskItems.FirstOrDefaultAsync();
            if (data == null) return NotFound();
            _context.TaskItems.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
