using Application.Service.Interface;
using EFCore;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return new List<SubCategory>();
    }
    public async Task<SubCategory> GetSubCategoryByIdAsync(int id)
    {
        return new SubCategory();
    }
    public async Task CreateSubCategoryAsync(SubCategory subCategory)
    {
        // Logic to create a new subcategory
    }
    public async Task UpdateSubCategoryAsync(int id, SubCategory subCategory)
    {
        // Logic to update an existing subcategory
    }
    public async Task DeleteSubCategoryAsync(int id)
    {
        // Logic to delete a subcategory
    }
}
