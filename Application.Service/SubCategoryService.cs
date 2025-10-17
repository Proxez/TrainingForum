using Application.Service.Interface;
using Entities;

namespace Application.Service;
public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _repo;

    public SubCategoryService(ISubCategoryRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
    {
        return await _repo.GetAllSubCategoriesAsync();
    }
    public async Task<SubCategory> GetSubCategoryByIdAsync(int id)
    {
        return await _repo.GetSubCategoryByIdAsync(id);
    }
    public async Task CreateSubCategoryAsync(SubCategory subCategory)
    {
        await _repo.AddSubCategoryAsync(subCategory);
    }
    public async Task UpdateSubCategoryAsync(int id, SubCategory subCategory)
    {
        await _repo.UpdateSubCategoryAsync(id, subCategory);
    }
    public async Task DeleteSubCategoryAsync(int id)
    {
        var category = await _repo.GetSubCategoryByIdAsync(id);
        await _repo.DeleteSubCategoryAsync(category);
    }
    public Task<SubCategory?> GetByIdWithPostsAsync(int id)
       => _repo.GetByIdWithPostsAsync(id);
}
