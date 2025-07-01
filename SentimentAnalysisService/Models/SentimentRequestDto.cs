namespace SentimentAnalysisService.Models
{
    public class SentimentRequestDto
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

    public class SentimentResultDto
    {
        public string Sentiment { get; set; }
        public double Score { get; set; }
        public DateTime AnalyzedAt { get; set; }
    }
}
