using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Controllers;

public class HomeController(TaskManagementDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _context.Users.ToListAsync());
    }
    public async Task<IActionResult> Chat()
    {
        return View(await _context.Users.Select(x => new AppUser
        {
            UserName = x.UserName,
            Email = x.Email,
            ProfileImagerlUrl = x.ProfileImagerlUrl,
            FullName = x.FullName,
        }).ToListAsync());
    }

}
