using System.Threading.Tasks;
using TradeLoggingService.Models;
using Microsoft.Extensions.Logging;

namespace TradeLoggingService.Services
{
    public class TradeLogger : ITradeLogger
    {
        private readonly ILogger<TradeLogger> _logger;
        public TradeLogger(ILogger<TradeLogger> logger)
        {
            _logger = logger;
        }

        public Task LogTradeAsync(TradeDto trade)
        {
            _logger.LogInformation($"Trade logged: {trade.TradeId} {trade.Symbol} {trade.Side} {trade.Quantity} @ {trade.Price} on {trade.Timestamp}");
            return Task.CompletedTask;
        }
    }
}
