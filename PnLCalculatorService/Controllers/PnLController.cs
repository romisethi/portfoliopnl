using Microsoft.AspNetCore.Mvc;
using PnLCalculatorService.Services;
using PnLCalculatorService.Models;

namespace PnLCalculatorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PnLController : ControllerBase
    {
        private readonly IPnLCalculator _pnlCalculator;
        public PnLController(IPnLCalculator pnlCalculator)
        {
            _pnlCalculator = pnlCalculator;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("PnLCalculatorService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] PnLRequestDto request)
        {
            var result = await _pnlCalculator.CalculateAsync(request);
            return Ok(result);
        }
    }
}
