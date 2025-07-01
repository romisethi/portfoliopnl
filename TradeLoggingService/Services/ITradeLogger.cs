using System.Threading.Tasks;
using TradeLoggingService.Models;

namespace TradeLoggingService.Services
{
    public interface ITradeLogger
    {
        Task LogTradeAsync(TradeDto trade);
    }
}
