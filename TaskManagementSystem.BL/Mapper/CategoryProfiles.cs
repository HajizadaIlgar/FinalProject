using AutoMapper;
using TaskManagementSystem.BL.DTOs.CategoryDTOs;
using TaskManagementSystem.CORE.Entities.Categories;

namespace TaskManagementSystem.BL.Mapper
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryListitem, Category>().ReverseMap();
        }
    }
}
