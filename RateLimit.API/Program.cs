using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Enable support for IOptions<T> to bind configuration sections to strongly typed classes
builder.Services.AddOptions();


// Add in-memory caching service (used to store rate limiting counters and policies in memory)
builder.Services.AddMemoryCache();

#region Ip Rate Limit
// Bind "IpRateLimiting" section from appsettings.json to IpRateLimitOptions class
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

// Bind "IpRateLimitPolicies" section from appsettings.json to IpRateLimitPolicies class
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// Store IP policies (rules for specific IPs) in memory cache
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
#endregion

#region Client Rate Limit
builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));
builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
#endregion

// Store rate limit counters (number of requests per IP) in memory cache
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

// Provides general rate limiting configuration (e.g., rules, resolvers)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Makes HttpContext (e.g., client IP, request info) accessible within services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Rate Limit policy seed action
using (var scope = app.Services.CreateScope())
{
    var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();
    ipPolicyStore.SeedAsync().GetAwaiter().GetResult();

    var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();
    clientPolicyStore.SeedAsync().GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();