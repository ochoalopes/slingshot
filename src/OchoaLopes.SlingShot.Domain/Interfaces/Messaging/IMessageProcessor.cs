namespace OchoaLopes.SlingShot.Domain.Interfaces.Messaging
{
    public interface IMessageProcessor
    {
        Task ProcessMessagesAsync(CancellationToken cancellationToken);
    }
}
