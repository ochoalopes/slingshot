using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Messaging;

namespace OchoaLopes.SlingShot.Infra.Messaging.Target
{
    public class KafkaMessageTarget : IMessageTarget
    {
        public Task SendMessageAsync(MessageEntity message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SendMessagesAsync(IEnumerable<MessageEntity> messages, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
