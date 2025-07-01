using Microsoft.AspNetCore.Mvc;
using AuditLoggingService.Services;
using AuditLoggingService.Models;

namespace AuditLoggingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditLogger _auditLogger;
        public AuditController(IAuditLogger auditLogger)
        {
            _auditLogger = auditLogger;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("AuditLoggingService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("log")]
        public async Task<IActionResult> Log([FromBody] AuditLogDto log)
        {
            await _auditLogger.LogAsync(log);
            return Ok(new { status = "Audit log recorded", log });
        }
    }
}
