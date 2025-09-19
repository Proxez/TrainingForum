using Entites;

namespace Application.Service.Interface;
public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task UpdateUserAsync(int id, User user);
}