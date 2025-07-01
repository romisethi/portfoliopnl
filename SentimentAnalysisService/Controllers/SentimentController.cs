using Microsoft.AspNetCore.Mvc;
using SentimentAnalysisService.Services;
using SentimentAnalysisService.Models;
using Shared;

namespace SentimentAnalysisService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentController : ControllerBase
    {
        private readonly ISentimentAnalysisService _sentimentService;
        private readonly IAuditLoggingClient _auditLogger;
        public SentimentController(ISentimentAnalysisService sentimentService, IAuditLoggingClient auditLogger)
        {
            _sentimentService = sentimentService;
            _auditLogger = auditLogger;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("SentimentAnalysisService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] SentimentRequestDto request)
        {
            var result = await _sentimentService.AnalyzeAsync(request);
            // Example: log the sentiment analysis event
            await _auditLogger.LogAsync(new {
                Action = "SentimentAnalyzed",
                Input = request,
                Result = result,
                Timestamp = DateTime.UtcNow
            }, HttpContext.TraceIdentifier);
            return Ok(result);
        }
    }
}
