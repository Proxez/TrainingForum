using Entities;

namespace Application.Service.Interface;
public interface IReactionService
{
    Task CreateReactionAsync(Reaction reaction);
    Task DeleteReactionAsync(int id);
    Task<List<Reaction>> GetAllReactionsAsync();
    Task<Reaction> GetReactionByIdAsync(int id);
    Task UpdateReactionAsync(int id, Reaction reaction);
}