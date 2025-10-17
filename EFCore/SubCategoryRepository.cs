using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly MyDbContext _context;
    public SubCategoryRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
    {
        return await _context.SubCategories.ToListAsync();
    }
    public async Task<SubCategory?> GetSubCategoryByIdAsync(int id)
    {
        return await _context.SubCategories.FindAsync(id);
    }
    public async Task AddSubCategoryAsync(SubCategory subCategory)
    {
        await _context.SubCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateSubCategoryAsync(int id, SubCategory updatedSubCategory)
    {
        var subCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == id);
        if (subCategory != null)
        {
            subCategory.CategoryId = updatedSubCategory.CategoryId;
            subCategory.Title = updatedSubCategory.Title;
            subCategory.Description = updatedSubCategory.Description;
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteSubCategoryAsync(SubCategory subCategory)
    {
        if (subCategory != null)
        {
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<SubCategory?> GetByIdWithPostsAsync(int id)
    {
        return await _context.SubCategories
            .AsNoTracking()
            .Include(sc => sc.Posts)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(sc => sc.Id == id);
    }
}
