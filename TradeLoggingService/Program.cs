using Serilog;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.ServiceBus;
using System.Net.Http;
using Shared;
using TradeLoggingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// Polly HttpClient policy registration is not available for .NET 9.0 at this time.
// To add retry/circuit breaker, use Polly.Extensions.Http when .NET 9.0 support is available.
builder.Services.AddHttpClient("DefaultClient");

// Add OpenAPI/Swagger
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAuditLoggingClient(builder.Configuration);
builder.Services.AddScoped<ITradeLogger, TradeLogger>();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Custom CorrelationId middleware
app.Use(async (context, next) =>
{
    const string header = "X-Correlation-ID";
    if (!context.Request.Headers.TryGetValue(header, out var correlationId))
    {
        correlationId = Guid.NewGuid().ToString();
        context.Request.Headers[header] = correlationId;
    }
    context.Response.Headers[header] = correlationId;
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
