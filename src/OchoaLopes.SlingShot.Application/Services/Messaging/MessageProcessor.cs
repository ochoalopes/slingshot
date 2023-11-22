using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using OchoaLopes.SlingShot.Domain.Interfaces.Messaging;
using System.Diagnostics;

namespace OchoaLopes.SlingShot.Application.Services.Messaging
{
    public class MessageProcessor : IMessageProcessor
    {
        #region Properties
        private readonly ILogger<MessageProcessor> _logger;
        private readonly IMessageSource _source;
        private readonly IMessageTarget _target;
        private readonly TelemetryClient _telemetryClient;
        #endregion

        #region Public Methods
        public MessageProcessor(ILogger<MessageProcessor> logger, IMessageSource source, IMessageTarget target, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _source = source;
            _target = target;
            _telemetryClient = telemetryClient;
        }

        public async Task ProcessMessagesAsync(CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            var messages = await _source.ReadMessagesAsync(cancellationToken);

            foreach (var message in messages)
            {
                stopwatch.Start();

                try
                {
                    await _target.SendMessageAsync(message, cancellationToken);
                    _logger.LogInformation("Message processed: {0}", message);

                    stopwatch.Stop();

                    _telemetryClient.TrackMetric("MessageProcessingTime", stopwatch.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message {message}", message);

                    _telemetryClient.TrackMetric("MessageProcessingError", 1);
                }
                finally
                {
                    stopwatch.Stop();
                }
            }
        }
        #endregion
    }
}
