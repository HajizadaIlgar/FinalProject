using TaskManagementSystem.CORE.Entities.Categories;

namespace TaskManagementSystem.BL.DTOs.CategoryDTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public static implicit operator Category(CategoryCreateDto dto)
        {
            Category category = new Category
            {
                Name = dto.Name,
            };
            return category;
        }
    }
}
