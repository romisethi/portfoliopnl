namespace AuditLoggingService.Models
{
    public class AuditLogDto
    {
        public string Action { get; set; }
        public string User { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
