namespace AlertingService.Models
{
    public class AlertDto
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
