using Entites;

namespace Application.Service.Interface;
public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentByIdAsync(int id);
    Task UpdateCommentAsync(int id, Comment comment);
}