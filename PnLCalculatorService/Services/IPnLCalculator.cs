using System.Threading.Tasks;
using PnLCalculatorService.Models;

namespace PnLCalculatorService.Services
{
    public interface IPnLCalculator
    {
        Task<PnLResultDto> CalculateAsync(PnLRequestDto request);
    }
}
