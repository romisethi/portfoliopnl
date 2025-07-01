using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using AuditLoggingService.Models;

namespace AuditLoggingService.Services
{
    public class CosmosAuditLogRepository
    {
        private readonly Container _container;
        public CosmosAuditLogRepository(IConfiguration config)
        {
            var endpoint = config["CosmosDb:AccountEndpoint"];
            var key = config["CosmosDb:AccountKey"];
            var dbName = config["CosmosDb:DatabaseName"];
            var containerName = config["CosmosDb:ContainerName"];
            var client = new CosmosClient(endpoint, key);
            _container = client.GetContainer(dbName, containerName);
        }

        public async Task SaveLogAsync(AuditLogDto log)
        {
            await _container.CreateItemAsync(log, new PartitionKey(log.Entity ?? "none"));
        }
    }
}
