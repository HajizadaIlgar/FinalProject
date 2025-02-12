using TaskManagementSystem.BL.DTOs.CategoryDTOs;
using TaskManagementSystem.CORE.Entities.Users;

namespace TaskManagementSystem.BL.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<List<CategoryListitem>> GetAllCategories();
        Task<int> CreateCategory(CategoryCreateDto dto);
        Task<int> UpdateCategory(CategoryCreateDto dto, int id);
        Task<int> DeleteCategory(int id);
        Task<AppUser> GetCurrentUserAsync();
    }
}
