using Entites;

namespace Application.Service.Interface;
public interface IReactionRepository
{
    Task AddReactionAsync(Reaction reaction);
    Task DeleteReactionAsync(Reaction reaction);
    Task<List<Reaction>> GetAllReactionsAsync();
    Task<Reaction?> GetReactionByIdAsync(int id);
    Task UpdateReactionAsync(int id, Reaction reaction);
}