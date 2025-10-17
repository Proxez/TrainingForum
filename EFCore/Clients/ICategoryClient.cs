using Application.Service.DTOs;
using Entities;

namespace EFCore.Clients;
public interface ICategoryClient
{
    Task<CategoryDto> CreateCategoryAsync(Category category, CancellationToken ct = default);
    Task<List<Category>> GetAllCategoriesAsync(string uri);
    Task<Category> GetCategoryByIdAsync(string uri);
    Task<Category?> GetCategoryWithSubcatsAndPostsAsync(int id);
    Task UpdateCategoryAsync(int id, Category category);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken ct = default);
}