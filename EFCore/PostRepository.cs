using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class PostRepository : IPostRepository
{
    private readonly MyDbContext _context;
    public PostRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }
    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }
    public async Task<List<Post>> GetPagedPostsAsync(int page, int pageSize)
    {
        return await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.SubCategory)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task AddPostAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }
    public async Task UpdatePostAsync(int id, Post updatedPost)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(c => c.Id == id);
        if (post != null)
        {
            post.Content = updatedPost.Content;
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeletePostAsync(Post post)
    {
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }    
}
