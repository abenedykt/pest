using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using pest.tracking;

var builder = WebApplication.CreateBuilder(args);
// logging
builder.Logging.ClearProviders()
    .AddOpenTelemetry(x =>
    {
        x.SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService("pest.tracking"));
        x.AddConsoleExporter();
        x.AddOtlpExporter(c =>
        {
            c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
            c.Protocol = OtlpExportProtocol.HttpProtobuf;
        });
    });


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "zimno", "Bracing", "Chilly", "spoko", "Mild", "ciepło", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();