using Application.Service.Interface;
using Entities;

namespace Application.Service;
public class CommentService : ICommentService
{
    private readonly ICommentRepository _repo;

    public CommentService(ICommentRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _repo.GetAllCommentsAsync();
    }
    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _repo.GetCommentByIdAsync(id);
    }
    public async Task CreateCommentAsync(Comment comment)
    {
        await _repo.AddCommentAsync(comment);
    }
    public async Task UpdateCommentAsync(int id, Comment comment)
    {
        await _repo.UpdateCommentAsync(id, comment);
    }
    public async Task DeleteCommentAsync(int id)
    {
        var comment = await _repo.GetCommentByIdAsync(id);
        await _repo.DeleteCommentAsync(comment);
    }
}
