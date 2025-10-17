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
            _context.Posts.Update(updatedPost);
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
