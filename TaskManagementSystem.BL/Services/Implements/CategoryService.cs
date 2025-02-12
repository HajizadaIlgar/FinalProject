using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementSystem.BL.DTOs.CategoryDTOs;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Entities.Categories;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.CORE.Repositories;

namespace TaskManagementSystem.BL.Services.Implements
{
    public class CategoryService(ICategoryRepository _repo, IMapper _mapper, IHttpContextAccessor _httpContextAccessor, UserManager<AppUser> _userManager) : ICategoryService
    {
        public async Task<AppUser> GetCurrentUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return null; // İstifadəçi yoxdursa, null qaytarırıq
            }

            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
        public async Task<int> CreateCategory(CategoryCreateDto dto)
        {
            Category category = dto;
            await _repo.AddAsync(dto);
            await _repo.SaveAsync();
            return category.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var data = await _repo.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null)
            {
                throw new Exception();
            }
            _repo.Delete(data);
            await _repo.SaveAsync();
            return data.Id;
        }

        public async Task<List<CategoryListitem>> GetAllCategories()
        {
            var datas = _repo.GetAll().Select(x => new CategoryListitem
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return datas.Result;
        }

        public async Task<int> UpdateCategory(CategoryCreateDto dto, int id)
        {
            var data = await _repo.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null)
            {
                throw new Exception();
            }
            data.Name = dto.Name;
            await _repo.SaveAsync();
            return data.Id;
        }

    }
}
