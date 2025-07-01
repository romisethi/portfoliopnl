using System.Threading.Tasks;
using SentimentAnalysisService.Models;

namespace SentimentAnalysisService.Services
{
    public interface ISentimentAnalysisService
    {
        Task<SentimentResultDto> AnalyzeAsync(SentimentRequestDto request);
    }
}
