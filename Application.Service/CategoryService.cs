using Application.Service.Interface;
using Entites;

namespace Application.Service;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _repo.GetCategoryByIdAsync(id);
    }
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _repo.GetAllCategoriesAsync();
    }
    public async Task CreateCategoryAsync(Category category)
    {
        await _repo.AddCategoryAsync(category);
    }
    public async Task UpdateCategoryAsync(int id, Category category)
    {
        await _repo.UpdateCategoryAsync(id,category);
    }
    public async Task DeleteCategoryAsync(int id)
    {
        Category category = await _repo.GetCategoryByIdAsync(id);
    }
}
