using System.Threading.Tasks;
using NotificationService.Models;
using Microsoft.Extensions.Logging;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(NotificationDto notification)
        {
            _logger.LogInformation($"Notification sent: {notification.NotificationId} to {notification.Recipient} via {notification.Channel} at {notification.Timestamp}: {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
