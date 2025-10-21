using Entities;

namespace Application.Service.Interface;
public interface IMessageService
{
    Task<(IReadOnlyList<Message> items, int unread)> GetInboxAsync(int userId, int skip = 0, int take = 50, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetThreadAsync(int userId, int otherUserId, CancellationToken ct = default);
    Task SendAsync(int senderId, int recipientId, string body, CancellationToken ct = default);
}