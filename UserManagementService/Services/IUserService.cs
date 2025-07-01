using System.Threading.Tasks;
using UserManagementService.Models;

namespace UserManagementService.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterUserDto user);
    }
}
