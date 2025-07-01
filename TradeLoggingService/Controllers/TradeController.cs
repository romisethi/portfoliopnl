using Microsoft.AspNetCore.Mvc;
using TradeLoggingService.Services;
using TradeLoggingService.Models;

namespace TradeLoggingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeLogger _tradeLogger;
        public TradeController(ITradeLogger tradeLogger)
        {
            _tradeLogger = tradeLogger;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("TradeLoggingService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("log")]
        public async Task<IActionResult> LogTrade([FromBody] TradeDto trade)
        {
            await _tradeLogger.LogTradeAsync(trade);
            return Ok(new { status = "Trade logged", trade });
        }
    }
}
