using Microsoft.AspNetCore.Mvc;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Cosmos;

namespace MarketDataTransformerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransformerController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("MarketDataTransformerService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");
    }
}
