using Entities;

namespace Application.Service.Interface;
public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment);
    Task DeleteCommentAsync(Comment comment);
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentByIdAsync(int id);
    Task UpdateCommentAsync(int id, Comment comment);
    Task<List<Comment>> GetAllCommentsByPostIdAsync(int id);
}