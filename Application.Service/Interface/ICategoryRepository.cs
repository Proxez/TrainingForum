using Entities;

namespace Application.Service.Interface;
public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task UpdateCategoryAsync(int id, Category category);
}