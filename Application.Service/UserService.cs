using Application.Service.Interface;
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
        return await _repo.GetAllUsersAsync();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _repo.GetUserByIdAsync(id);
    }
    public async Task CreatePostAsync(User User)
    {
        await _repo.AddUserAsync(User);
    }
    public async Task UpdateUserAsync(int id, User User)
    {
        await _repo.UpdateUserAsync(id, User);
    }
    public async Task DeleteUserAsync(int id)
    {
        var user = await _repo.GetUserByIdAsync(id);
        await _repo.DeleteUserAsync(user);
    }
}
