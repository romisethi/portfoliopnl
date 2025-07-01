using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlertingService.Services;
using AlertingService.Models;

namespace AlertingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("AlertingService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("trigger")]
        public async Task<IActionResult> TriggerAlert([FromBody] AlertDto alert)
        {
            await _alertService.TriggerAlertAsync(alert);
            return Ok(new { status = "Alert triggered", alert });
        }
    }
}
