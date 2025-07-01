using Microsoft.AspNetCore.Mvc;
using UserManagementService.Models;
using UserManagementService.Services;

namespace UserManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("UserManagementService is running");

        [HttpGet("health")]
        public IActionResult Health() => Ok("OK");

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            var result = await _userService.RegisterAsync(user);
            return Ok(result);
        }
    }
}
