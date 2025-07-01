using System.Threading.Tasks;
using PnLCalculatorService.Models;
using Microsoft.Extensions.Logging;

namespace PnLCalculatorService.Services
{
    public class PnLCalculator : IPnLCalculator
    {
        private readonly ILogger<PnLCalculator> _logger;
        public PnLCalculator(ILogger<PnLCalculator> logger)
        {
            _logger = logger;
        }

        public Task<PnLResultDto> CalculateAsync(PnLRequestDto request)
        {
            _logger.LogInformation($"PnL calculated for {request.PortfolioId} {request.Symbol} {request.Quantity} @ {request.Price} on {request.Timestamp}");
            // Stub result
            return Task.FromResult(new PnLResultDto {
                PortfolioId = request.PortfolioId,
                Symbol = request.Symbol,
                RealizedPnL = 0,
                UnrealizedPnL = 0,
                CalculatedAt = DateTime.UtcNow
            });
        }
    }
}
