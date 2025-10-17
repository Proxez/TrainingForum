using Application.Service.Interface;
using EFCore.Clients;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class SubCategoryAPIRepository : ISubCategoryRepository
{
    private readonly ISubCategoryClient _client;

    public SubCategoryAPIRepository(ISubCategoryClient client)
    {
        _client = client;
    }
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
    {
        return await _client.GetAllSubCategoriesAsync("/api/subcategory");
    }

    public async Task<SubCategory?> GetSubCategoryByIdAsync(int subCategoryId)
    {
        return await _client.GetCategoryByIdAsync($"{subCategoryId}");
    }
    public Task AddSubCategoryAsync(SubCategory subCategory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSubCategoryAsync(SubCategory subCategory)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSubCategoryAsync(int id, SubCategory updatedSubCategory)
    {
        throw new NotImplementedException();
    }

    public async Task<SubCategory?> GetByIdWithPostsAsync(int id)
    {
        return await _client.GetByIdWithPostsAsync(id);
    }
}
