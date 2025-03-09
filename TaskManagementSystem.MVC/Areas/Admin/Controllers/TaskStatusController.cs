using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.TaskDTOs;
using TaskManagementSystem.CORE.Entities.Tasks;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TaskStatusController(TaskManagementDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskItemsStatus.ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskStatusCreateDto statusDto)
        {
            TaskItemStatus taskStatus = new TaskItemStatus
            {
                TaskStatus = statusDto.TaskStatus,
                CreatedAt = DateTime.UtcNow,
            };
            await _context.TaskItemsStatus.AddAsync(taskStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.TaskItemsStatus.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null) return NotFound();
            _context.TaskItemsStatus.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
