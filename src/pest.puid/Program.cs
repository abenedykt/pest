using HealthChecks.UI.Client;
using IdGen;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIdGenerator<long>>(new IdGenerator(0));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/", (IIdGenerator<long> g) => g.CreateId()).WithOpenApi();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
    
public partial class Program {
    // needed for the sake of running pest.puid.tests using TestWebApplicationFactory
}