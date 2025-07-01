using System.Threading.Tasks;
using AuditLoggingService.Models;

namespace AuditLoggingService.Services
{
    public interface IAuditLogger
    {
        Task LogAsync(AuditLogDto log);
    }
}
