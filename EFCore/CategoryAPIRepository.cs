using Application.Service.Interface;
using EFCore.Clients;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class CategoryAPIRepository : ICategoryRepository
{
    private readonly ICategoryClient _client;

    public CategoryAPIRepository(ICategoryClient client)
    {
        _client = client;
    }
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _client.GetAllCategoriesAsync("api/category");
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await _client.GetCategoryByIdAsync($"api/category/{categoryId}");
    }

    public async Task<Category?> GetCategoryWithSubcatsAndPostsAsync(int id)
    {
        return await _client.GetCategoryWithSubcatsAndPostsAsync(id);
    }
    public async Task AddCategoryAsync(Category category)
    {
        await _client.CreateCategoryAsync(category);
    }

    public async Task UpdateCategoryAsync(int id, Category updatedCategory)
        => await _client.UpdateCategoryAsync(id, updatedCategory);

    public async Task DeleteCategoryAsync(Category category)
    {

        await _client.DeleteCategoryAsync(category.Id);
    }
}
