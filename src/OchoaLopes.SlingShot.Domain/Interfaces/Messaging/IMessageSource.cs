using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Domain.Interfaces.Messaging
{
    public interface IMessageSource
    {
        Task<MessageEntity> ReadMessageAsync(CancellationToken cancellationToken);
        Task<IEnumerable<MessageEntity>> ReadMessagesAsync(CancellationToken cancellationToken);
    }
}
