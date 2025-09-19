using Application.Service.Interface;
using Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class ReactionRepository : IReactionRepository
{
    private readonly MyDbContext _context;

    public ReactionRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Reaction>> GetAllReactionsAsync()
    {
        return await _context.Reactions.ToListAsync();
    }
    public async Task<Reaction?> GetReactionByIdAsync(int id)
    {
        return await _context.Reactions.FindAsync(id);
    }
    public async Task AddReactionAsync(Reaction reaction)
    {
        await _context.Reactions.AddAsync(reaction);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateReactionAsync(int id, Reaction updatedReaction)
    {
        var reaction = await _context.Reactions.FirstOrDefaultAsync(c => c.Id == id);
        if (reaction != null)
        {
            reaction.Type = updatedReaction.Type;
            _context.Reactions.Update(updatedReaction);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteReactionAsync(int id)
    {
        var reaction = await _context.Reactions.FindAsync(id);
        if (reaction != null)
        {
            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();
        }
    }
}
