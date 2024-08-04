using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.SemanticKernel;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using pest.pricing;
using Kernel = Microsoft.SemanticKernel.Kernel;

var builder = WebApplication.CreateBuilder(args);
// logging
builder.Logging.ClearProviders()
    .AddOpenTelemetry(x =>
    {
        x.SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService("pest.pricing"));
        x.AddConsoleExporter();
        x.AddOtlpExporter(c =>
        {
            c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
            c.Protocol = OtlpExportProtocol.HttpProtobuf;
        });
    });


// lets some AI Kernel

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var modelId = "phi3";
var ollamaUri = new Uri("http://localhost:11434");

var aiKernel = builder.Services.AddKernel();
    aiKernel.AddOpenAIChatCompletion(modelId, ollamaUri, apiKey: null);

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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapGet("/weatherforecast", async (Kernel ai) =>
{
        var temp = Random.Shared.Next(-20, 45);
        var text = await ai.InvokePromptAsync<string>($"Short weather description for {temp} degrees Celsius?");
        
        
        return new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now),
            temp,
            text
        );    
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

