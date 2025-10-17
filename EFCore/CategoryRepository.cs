using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;
    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }
    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCategoryAsync(int id, Category updatedCategory)
    {
        var category = await GetCategoryByIdAsync(id);
        if (category != null)
        {
            category.Title = updatedCategory.Title;
            category.Description = updatedCategory.Description;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteCategoryAsync(Category category)
    {
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Category?> GetCategoryWithSubcatsAndPostsAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Posts)
                    .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
