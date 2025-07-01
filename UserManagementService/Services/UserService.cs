using System.Threading.Tasks;
using UserManagementService.Models;
using Microsoft.Extensions.Logging;

namespace UserManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public Task<string> RegisterAsync(RegisterUserDto user)
        {
            _logger.LogInformation($"User registered: {user.UserId} {user.UserName} {user.Email} at {user.RegisteredAt}");
            return Task.FromResult("User registered successfully");
        }
    }
}
