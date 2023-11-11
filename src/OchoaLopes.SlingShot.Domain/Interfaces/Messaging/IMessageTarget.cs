using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Domain.Interfaces.Messaging
{
    public interface IMessageTarget
    {
        Task SendMessageAsync(MessageEntity message, CancellationToken cancellationToken);
        Task SendMessagesAsync(IEnumerable<MessageEntity> messages, CancellationToken cancellationToken);
    }
}
