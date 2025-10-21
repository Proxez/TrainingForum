using Application.Service.Interface;
using EFCore;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;
public class MessageService : IMessageService
{
    private readonly IMessageRepository _repo;
    public MessageService(IMessageRepository repo) => _repo = repo;

    public async Task SendAsync(int senderId, int recipientId, string body, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(body)) throw new ArgumentException("Body is required.", nameof(body));
        var msg = new Message
        {
            SenderId = senderId,
            RecipientId = recipientId,
            Body = body.Trim(),
            SentAtUtc = DateTimeOffset.UtcNow
        };
        await _repo.AddAsync(msg, ct);
        await _repo.SaveChangesAsync(ct);
    }

    public async Task<(IReadOnlyList<Message> items, int unread)> GetInboxAsync(int userId, int skip = 0, int take = 50, CancellationToken ct = default)
    {
        var items = await _repo.GetInboxAsync(userId, skip, take, ct);
        var unread = await _repo.CountUnreadAsync(userId, ct);
        return (items, unread);
    }

    public async Task<IReadOnlyList<Message>> GetThreadAsync(int userId, int otherUserId, CancellationToken ct = default)
    {
        // markera som läst för inkommande
        await _repo.MarkIncomingAsReadAsync(userId, otherUserId, ct);
        await _repo.SaveChangesAsync(ct);

        return await _repo.GetThreadAsync(userId, otherUserId, ct);
    }
}
