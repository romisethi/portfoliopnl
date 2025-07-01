using System.Threading.Tasks;
using AuditLoggingService.Models;
using Microsoft.Extensions.Logging;

namespace AuditLoggingService.Services
{
    public class AuditLogger : IAuditLogger
    {
        private readonly ILogger<AuditLogger> _logger;
        private readonly CosmosAuditLogRepository _cosmosRepo;
        public AuditLogger(ILogger<AuditLogger> logger, CosmosAuditLogRepository cosmosRepo)
        {
            _logger = logger;
            _cosmosRepo = cosmosRepo;
        }

        public async Task LogAsync(AuditLogDto log)
        {
            _logger.LogInformation($"Audit: {log.Action} by {log.User} on {log.Entity}({log.EntityId}) at {log.Timestamp}: {log.Details}");
            await _cosmosRepo.SaveLogAsync(log);
        }
    }
}
