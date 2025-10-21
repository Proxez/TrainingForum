using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class MessageRepository : IMessageRepository
{
    private readonly MyDbContext _db;
    public MessageRepository(MyDbContext db) => _db = db;

    public async Task AddAsync(Message message, CancellationToken ct = default)
        => await _db.Messages.AddAsync(message, ct);

    public async Task<IReadOnlyList<Message>> GetInboxAsync(int userId, int skip, int take, CancellationToken ct = default)
    {
        return await _db.Messages
            .AsNoTracking()
            .Where(m => m.RecipientId == userId)
            .OrderByDescending(m => m.SentAtUtc)
            .Skip(skip).Take(take)
            .Include(m => m.Sender)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Message>> GetThreadAsync(int userId, int otherUserId, CancellationToken ct = default)
    {
        return await _db.Messages
            .Where(m => (m.SenderId == userId && m.RecipientId == otherUserId) ||
                        (m.SenderId == otherUserId && m.RecipientId == userId))
            .OrderBy(m => m.SentAtUtc)
            .ToListAsync(ct);
    }

    public async Task<int> CountUnreadAsync(int userId, CancellationToken ct = default)
        => await _db.Messages.CountAsync(m => m.RecipientId == userId && !m.IsRead, ct);

    public async Task MarkIncomingAsReadAsync(int userId, int otherUserId, CancellationToken ct = default)
    {
        var unread = await _db.Messages
            .Where(m => m.RecipientId == userId && m.SenderId == otherUserId && !m.IsRead)
            .ToListAsync(ct);

        foreach (var m in unread) m.IsRead = true;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
