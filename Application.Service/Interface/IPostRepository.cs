using Entities;

namespace Application.Service.Interface;
public interface IPostRepository
{
    Task AddPostAsync(Post post);
    Task DeletePostAsync(Post post);
    Task<List<Post>> GetAllPostsAsync();
    Task<List<Post>> GetPagedPostsAsync(int page, int pageSize);
    Task<Post?> GetPostByIdAsync(int id);
    Task UpdatePostAsync(int id, Post post);
}