using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.HomeDTOs;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Controllers;

public class HomeController(TaskManagementDbContext _context, UserManager<AppUser> _userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        return View(dto);
    }

    public async Task<IActionResult> Chat()
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        return View(dto);
    }
    public async Task<IActionResult> Calendars()
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        dto.Appointments = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();
        await Calendar();
        return View(dto);  // View qaytarılır
    }
    [HttpGet]
    [Route("/api/appointment")]
    public async Task<IActionResult> Calendar()
    {
        HomeDto dto = new HomeDto();
        dto.Appointments = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();

        return Json(dto);  // JSON formatında cavab qaytarır
    }

    public async Task<IActionResult> KanbanBoard(int taskId)
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        dto.Appointments = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();
        dto.TaskAssignments = await _context.TaskAssignments.Include(x => x.User).ToListAsync();
        dto.TaskItem = await _context.TaskItems.Include(t => t.Status).Include(x => x.TaskAssignments)
                .Include(t => t.Users).Where(x => !x.IsDeleted).ToListAsync();
        return View(dto);
    }
}
