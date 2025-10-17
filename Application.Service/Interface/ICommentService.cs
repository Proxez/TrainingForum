using Entities;

namespace Application.Service.Interface;
public interface ICommentService
{
    Task CreateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment> GetCommentByIdAsync(int id);
    Task<List<Comment>> GetAllCommentsByPostIdAsync(int id);
    Task UpdateCommentAsync(int id, Comment comment);
}