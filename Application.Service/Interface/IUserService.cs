using Entites;

namespace Application.Service.Interface;
public interface IUserService
{
    Task CreatePostAsync(User User);
    Task DeleteUserAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task UpdateUserAsync(int id, User User);
}