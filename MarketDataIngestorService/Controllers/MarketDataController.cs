using Microsoft.AspNetCore.Mvc;
using MarketDataIngestorService.Services;

namespace MarketDataIngestorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketDataController : ControllerBase
    {
        private readonly MarketDataPublisher _publisher;
        private readonly AlphaVantageClient _alphaVantage;
        public MarketDataController(MarketDataPublisher publisher, AlphaVantageClient alphaVantage)
        {
            _publisher = publisher;
            _alphaVantage = alphaVantage;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("MarketDataIngestorService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("ingest")]
        public async Task<IActionResult> Ingest()
        {
            var marketData = new
            {
                Symbol = "AAPL",
                Price = 195.23,
                Timestamp = DateTime.UtcNow
            };
            await _publisher.PublishAsync(marketData);
            return Ok(new { status = "Published to Service Bus", data = marketData });
        }

        [HttpPost("ingest/alphavantage")]
        public async Task<IActionResult> IngestAlphaVantage([FromQuery] string symbol)
        {
            var json = await _alphaVantage.GetIntradayAsync(symbol);
            if (json == null)
                return Problem("Failed to fetch data from Alpha Vantage");
            await _publisher.PublishRawAsync(json);
            return Ok(new { status = "Published to Service Bus", symbol });
        }
    }
}
