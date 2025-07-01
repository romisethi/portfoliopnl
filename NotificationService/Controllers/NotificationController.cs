using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("NotificationService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDto notification)
        {
            await _notificationService.SendAsync(notification);
            return Ok(new { status = "Notification sent", notification });
        }
    }
}
