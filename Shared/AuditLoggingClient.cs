using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;

namespace Shared
{
    public interface IAuditLoggingClient
    {
        Task LogAsync(object log, string correlationId = null);
    }

    public class AuditLoggingClient : IAuditLoggingClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _auditLogEndpoint;
        public AuditLoggingClient(HttpClient httpClient, string auditLogEndpoint)
        {
            _httpClient = httpClient;
            _auditLogEndpoint = auditLogEndpoint;
        }

        public async Task LogAsync(object log, string correlationId = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _auditLogEndpoint)
            {
                Content = JsonContent.Create(log)
            };
            if (!string.IsNullOrWhiteSpace(correlationId))
                request.Headers.Add("X-Correlation-ID", correlationId);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
