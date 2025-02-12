using TaskManagementSystem.CORE.Entities.Categories;
using TaskManagementSystem.CORE.Repositories;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(TaskManagementDbContext _context) : base(_context) { }
}
