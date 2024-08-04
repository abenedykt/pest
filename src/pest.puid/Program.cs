using HealthChecks.UI.Client;
using IdGen;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// logging
builder.Logging.ClearProviders()
    .AddOpenTelemetry(x =>
    {
        x.SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService("pest.puid"));
        x.AddConsoleExporter();
        x.AddOtlpExporter(c =>
        {
            c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
            c.Protocol = OtlpExportProtocol.HttpProtobuf;
        });
    });


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