namespace PnLCalculatorService.Models
{
    public class PnLRequestDto
    {
        public string PortfolioId { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class PnLResultDto
    {
        public string PortfolioId { get; set; }
        public string Symbol { get; set; }
        public decimal RealizedPnL { get; set; }
        public decimal UnrealizedPnL { get; set; }
        public DateTime CalculatedAt { get; set; }
    }
}
