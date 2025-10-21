using Entities;

namespace EFCore;
public interface IMessageRepository
{
    Task AddAsync(Message message, CancellationToken ct = default);
    Task<int> CountUnreadAsync(int userId, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetInboxAsync(int userId, int skip, int take, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetThreadAsync(int userId, int otherUserId, CancellationToken ct = default);
    Task MarkIncomingAsReadAsync(int userId, int otherUserId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}