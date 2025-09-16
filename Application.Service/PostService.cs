using Application.Service.Interface;
using EFCore;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;
public class PostService : IPostService
{
    private readonly IPostRepository _repo;

    public PostService(IPostRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _repo.GetAllPostsAsync();
    }
    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _repo.GetPostByIdAsync(id);
    }
    public async Task CreatePostAsync(Post post)
    {
        await _repo.AddPostAsync(post);
    }
    public async Task UpdatePostAsync(int id, Post post)
    {
        await _repo.UpdatePostAsync(id, post);
    }
    public async Task DeletePostAsync(int id)
    {
        await _repo.DeletePostAsync(id);
    }
}
