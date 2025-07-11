using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace Shared
{
    public static class AuditLoggingClientExtensions
    {
        public static IServiceCollection AddAuditLoggingClient(this IServiceCollection services, IConfiguration config)
        {
            var options = new AuditLoggingOptions();
            config.GetSection("AuditLogging").Bind(options);
            if (string.IsNullOrWhiteSpace(options.BaseUrl))
                throw new ArgumentException("AuditLogging:BaseUrl must be configured in appsettings.json");
            var endpoint = options.BaseUrl.TrimEnd('/') + options.LogEndpoint;
            services.AddHttpClient<IAuditLoggingClient, AuditLoggingClient>(client =>
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            });
            services.AddSingleton<IAuditLoggingClient>(sp =>
                new AuditLoggingClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IAuditLoggingClient)), endpoint));
            return services;
        }
    }
}
