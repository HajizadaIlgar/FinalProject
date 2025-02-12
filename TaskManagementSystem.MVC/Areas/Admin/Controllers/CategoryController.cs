using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL.DTOs.CategoryDTOs;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers;
[Area(nameof(Admin))]
public class CategoryController(ICategoryService _service, TaskManagementDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllCategories());
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        await _service.CreateCategory(dto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (id == null) return BadRequest();
        var data = await _context.Categories.FindAsync(id.Value);
        if (data == null) return BadRequest();
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, CategoryCreateDto dto)
    {
        if (id == null) return BadRequest();
        var data = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (data == null) return BadRequest();
        if (data is not null)
        {
            data.Name = dto.Name;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return BadRequest();
        await _service.DeleteCategory(id.Value);
        return RedirectToAction(nameof(Index));
    }
}
