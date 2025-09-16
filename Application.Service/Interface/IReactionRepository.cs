using Entites;

namespace EFCore;
public interface IReactionRepository
{
    Task AddReactionAsync(Reaction reaction);
    Task DeleteReactionAsync(int id);
    Task<List<Reaction>> GetAllReactionsAsync();
    Task<Reaction?> GetReactionByIdAsync(int id);
    Task UpdateReactionAsync(int id, Reaction reaction);
}