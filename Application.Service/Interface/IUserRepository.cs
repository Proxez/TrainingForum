using Entities;

namespace Application.Service.Interface;
public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task UpdateUserAsync(int id, User user);
}