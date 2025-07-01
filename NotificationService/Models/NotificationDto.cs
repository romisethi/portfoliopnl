namespace NotificationService.Models
{
    public class NotificationDto
    {
        public string NotificationId { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public string Channel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
