using HealthChecks.UI.Client;
using IdGen;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using pest.logging;

var builder = WebApplication.CreateBuilder(args);

// logging
builder.AddLogging();

builder.Services.AddSingleton<IIdGenerator<long>>(new IdGenerator(0));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", (IIdGenerator<long> g) => g.CreateId()).WithOpenApi();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
    
public partial class Program {
    // needed for the sake of running pest.puid.tests using TestWebApplicationFactory
}