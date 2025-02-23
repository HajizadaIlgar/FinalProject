using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.HomeDTOs;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Controllers;

public class HomeController(TaskManagementDbContext _context) : Controller
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

    [Route("api/appointment")]
    public async Task<IActionResult> Calendar()
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        dto.Appointments = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();

        return View(dto);
    }
    public async Task<IActionResult> KanbanBoard()
    {
        HomeDto dto = new HomeDto();
        dto.AppUsers = await _context.Users.ToListAsync();
        dto.Appointments = await _context.Appointments.Where(x => !x.IsDeleted).ToListAsync();
        return View(dto);
    }
}
