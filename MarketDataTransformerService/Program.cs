using Serilog;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.ServiceBus;
using System.Net.Http;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Cosmos;

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

var configuration = builder.Configuration;
var serviceBusConnectionString = configuration["ServiceBus:ConnectionString"];
var serviceBusTopic = configuration["ServiceBus:TopicName"];
var serviceBusSubscription = configuration["ServiceBus:SubscriptionName"];
var cosmosConnectionString = configuration["CosmosDb:ConnectionString"];
var cosmosDatabase = configuration["CosmosDb:DatabaseName"];
var cosmosContainer = configuration["CosmosDb:ContainerName"];

builder.Services.AddSingleton(_ => new ServiceBusClient(serviceBusConnectionString));
builder.Services.AddSingleton(_ => new CosmosClient(cosmosConnectionString));
builder.Services.AddControllers();

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

// Background service to process Service Bus messages
app.Lifetime.ApplicationStarted.Register(() =>
{
    var sbClient = app.Services.GetRequiredService<ServiceBusClient>();
    var cosmosClient = app.Services.GetRequiredService<CosmosClient>();
    var processor = sbClient.CreateProcessor(serviceBusTopic, serviceBusSubscription, new ServiceBusProcessorOptions());

    processor.ProcessMessageAsync += async args =>
    {
        var body = args.Message.Body.ToString();
        // Simulate transformation/filtering
        var transformed = body.ToUpperInvariant();
        // Save to Cosmos DB
        var container = cosmosClient.GetContainer(cosmosDatabase, cosmosContainer);
        await container.CreateItemAsync(new { id = Guid.NewGuid().ToString(), data = transformed });
        await args.CompleteMessageAsync(args.Message);
    };
    processor.ProcessErrorAsync += args => Task.CompletedTask;
    processor.StartProcessingAsync();
});

await app.RunAsync();
