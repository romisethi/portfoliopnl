using System.Threading.Tasks;
using SentimentAnalysisService.Models;
using Microsoft.Extensions.Logging;

namespace SentimentAnalysisService.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly ILogger<SentimentAnalysisService> _logger;
        public SentimentAnalysisService(ILogger<SentimentAnalysisService> logger)
        {
            _logger = logger;
        }

        public Task<SentimentResultDto> AnalyzeAsync(SentimentRequestDto request)
        {
            _logger.LogInformation($"Sentiment analyzed for text: {request.Text}");
            // Stub result
            return Task.FromResult(new SentimentResultDto {
                Sentiment = "neutral",
                Score = 0.5,
                AnalyzedAt = DateTime.UtcNow
            });
        }
    }
}
