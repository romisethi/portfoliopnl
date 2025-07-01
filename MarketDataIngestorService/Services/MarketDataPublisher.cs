using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace MarketDataIngestorService.Services
{
    public class MarketDataPublisher
    {
        private readonly ServiceBusClient _sbClient;
        private readonly string _topic;
        public MarketDataPublisher(ServiceBusClient sbClient, IConfiguration config)
        {
            _sbClient = sbClient;
            _topic = config["ServiceBus:TopicName"]!;
        }
        public async Task PublishAsync(object data)
        {
            var sender = _sbClient.CreateSender(_topic);
            var messageBody = JsonSerializer.Serialize(data);
            await sender.SendMessageAsync(new ServiceBusMessage(messageBody));
        }
        public async Task PublishRawAsync(string json)
        {
            var sender = _sbClient.CreateSender(_topic);
            await sender.SendMessageAsync(new ServiceBusMessage(json));
        }
    }
}
