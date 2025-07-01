using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace MarketDataIngestorService.Services
{
    public class AlphaVantageClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        public AlphaVantageClient(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config["AlphaVantage:ApiKey"] ?? "demo";
        }
        public async Task<string?> GetIntradayAsync(string symbol)
        {
            var httpClient = _httpClientFactory.CreateClient("DefaultClient");
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=1min&apikey={_apiKey}";
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadAsStringAsync();
        }
    }
}
