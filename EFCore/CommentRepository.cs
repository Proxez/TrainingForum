using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class CommentRepository : ICommentRepository
{
    private readonly MyDbContext _context;
    public CommentRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }
    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }
    public async Task AddCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCommentAsync(int id, Comment updatedComment)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment != null)
        {
            comment.Content = updatedComment.Content;
            _context.Comments.Update(updatedComment);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteCommentAsync(Comment comment)
    {
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
