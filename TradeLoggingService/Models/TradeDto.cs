namespace TradeLoggingService.Models
{
    public class TradeDto
    {
        public string TradeId { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Side { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
