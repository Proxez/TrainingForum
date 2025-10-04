using Entities;

namespace Application.Service.Interface;
public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task UpdateCategoryAsync(int id, Category category);
}