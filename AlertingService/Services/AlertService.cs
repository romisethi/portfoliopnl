using System;
using System.Threading.Tasks;
using AlertingService.Models;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using Microsoft.Extensions.Configuration;
using Azure;

namespace AlertingService.Services
{
    public class AlertService : IAlertService
    {
        private readonly ILogger<AlertService> _logger;
        private readonly EventGridPublisherClient _eventGridClient;
        private readonly string _topicEndpoint;
        public AlertService(ILogger<AlertService> logger, IConfiguration config)
        {
            _logger = logger;
            _topicEndpoint = config["EventGrid:TopicEndpoint"];
            var key = config["EventGrid:AccessKey"];
            if (string.IsNullOrWhiteSpace(_topicEndpoint) || string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("EventGrid:TopicEndpoint and EventGrid:AccessKey must be configured in appsettings.json");
            _eventGridClient = new EventGridPublisherClient(new Uri(_topicEndpoint), new AzureKeyCredential(key));
        }

        public async Task TriggerAlertAsync(AlertDto alert)
        {
            // Log locally
            _logger.LogInformation($"Alert triggered: {alert.Type} | {alert.Message} | {alert.Severity} | {alert.Timestamp}");

            // Publish to Event Grid
            var eventData = new EventGridEvent(
                subject: $"alert/{alert.Type}",
                eventType: "AlertTriggered",
                dataVersion: "1.0",
                data: alert
            );
            await _eventGridClient.SendEventAsync(eventData);
        }
    }
}
