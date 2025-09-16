using Application.Service.Interface;
using EFCore;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;
public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        return new List<User>();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return new User();
    }
    public async Task CreatePostAsync(User User)
    {
        // Logic to create a new post
    }
    public async Task UpdateUserAsync(int id, User User)
    {
        // Logic to update an existing post
    }
    public async Task DeleteUserAsync(int id)
    {
        // Logic to delete a post
    }
}
