using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Interfaces.Messaging;

namespace OchoaLopes.SlingShot.Infra.HostedService
{
    public class MessageProcessingHostedService : BackgroundService
    {
        private readonly ILogger<MessageProcessingHostedService> _logger;
        private readonly IMessageProcessor _messageProcessor;

        public MessageProcessingHostedService(ILogger<MessageProcessingHostedService> logger, IMessageProcessor messageProcessor)
        {
            _logger = logger;
            _messageProcessor = messageProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _messageProcessor.ProcessMessagesAsync(stoppingToken);
                //TODO: Logging and monitoring can be invoked here
                //TODO: This can be improved by using a timer instead of a delay and 
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}
