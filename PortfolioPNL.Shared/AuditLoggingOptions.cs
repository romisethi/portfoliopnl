namespace PortfolioPNL.Shared
{
    public class AuditLoggingOptions
    {
        public string BaseUrl { get; set; }
        public string LogEndpoint { get; set; } = "/api/audit/log";
    }
}
