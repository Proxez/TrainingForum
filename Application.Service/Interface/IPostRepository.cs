using Entites;

namespace Application.Service.Interface;
public interface IPostRepository
{
    Task AddPostAsync(Post post);
    Task DeletePostAsync(int id);
    Task<List<Post>> GetAllPostsAsync();
    Task<Post?> GetPostByIdAsync(int id);
    Task UpdatePostAsync(int id, Post post);
}