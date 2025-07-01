using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AspNetCoreRateLimit;
using GatewayService;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Ocelot and Rate Limiting
builder.Services.AddOcelot();
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false
            // Set your key/issuer/audience as needed
        };
    });

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

await app.UseOcelot();

await app.RunAsync();
