using OchoaLopes.SlingShot.Domain.Entities;
using OchoaLopes.SlingShot.Domain.Interfaces.Messaging;

namespace OchoaLopes.SlingShot.Infra.Messaging.Source
{
    public class StorageFileMessageSource : IMessageSource
    {
        public Task<MessageEntity> ReadMessageAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageEntity>> ReadMessagesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
