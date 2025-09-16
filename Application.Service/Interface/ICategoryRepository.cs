using Entites;

namespace EFCore;
public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task UpdateCategoryAsync(int id, Category category);
}