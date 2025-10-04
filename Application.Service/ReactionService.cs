using Application.Service.Interface;
using Entities;

namespace Application.Service;
public class ReactionService : IReactionService
{
    private readonly IReactionRepository _repo;

    public ReactionService(IReactionRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Reaction>> GetAllReactionsAsync()
    {
        return await _repo.GetAllReactionsAsync();
    }
    public async Task<Reaction> GetReactionByIdAsync(int id)
    {
        return await _repo.GetReactionByIdAsync(id);
    }
    public async Task CreateReactionAsync(Reaction reaction)
    {
        await _repo.AddReactionAsync(reaction);
    }
    public async Task UpdateReactionAsync(int id, Reaction reaction)
    {
        await _repo.UpdateReactionAsync(id, reaction);
    }
    public async Task DeleteReactionAsync(int id)
    {
        var reaction = await _repo.GetReactionByIdAsync(id);
        await _repo.DeleteReactionAsync(reaction);
    }
}
