using Entities;

namespace Application.Service.Interface;
public interface IPostService
{
    Task CreatePostAsync(Post post);
    Task DeletePostAsync(int id);
    Task<List<Post>> GetAllPostsAsync();
    Task<Post> GetPostByIdAsync(int id);
    Task UpdatePostAsync(int id, Post post);
}