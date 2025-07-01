using System.Threading.Tasks;
using AlertingService.Models;

namespace AlertingService.Services
{
    public interface IAlertService
    {
        Task TriggerAlertAsync(AlertDto alert);
    }
}
