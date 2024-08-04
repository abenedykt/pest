using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Pest.Parcel.Endpoints;
using Pest.Parcel.Extenstions;
using Pest.Parcel.Outbox;

var builder = WebApplication.CreateBuilder(args);

// logging
builder.Logging.ClearProviders()
    .AddOpenTelemetry(x =>
    {
        x.SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService("pest.parcel"));
        x.AddConsoleExporter();
        x.AddOtlpExporter(c =>
        {
            c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
            c.Protocol = OtlpExportProtocol.HttpProtobuf;
        });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddMinimalEndpoints();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddTransient<IOutbox, Outbox>();
builder.Services.AddTransient<IPuidClient, PuidClient>();
builder.Services.AddSingleton<IOutboxRepository, OutboxRepository>();
builder.Services.AddDbContext<OutboxDbContext>();

builder.Services.AddHostedService<Worker>(); // should be out of process - left here only for purpose of PoC.
                                             // We want to run app and outbox as separate process.
                                             // things to consider:
                                             //    - separate containers
                                             //    - separate scalability
                                             //    - separate deployment


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterMinimalEndpoints();
app.MapHealthChecks("/health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });



AutoMigrateDatabase(app);

app.Run();
return;

void AutoMigrateDatabase(WebApplication webApplication)
{
    // db auto upgrade -> not recommended for prod env. Its here only just out of laziness/convenience
    using var scope = webApplication.Services.CreateScope();
    
    var services = scope.ServiceProvider;
    var log = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<OutboxDbContext>();

        log.LogInformation("Looking for pending database migrations.");
        if (!context.Database.GetPendingMigrations().Any()) return;

        log.LogInformation("Found! Applying...");
        context.Database.Migrate();
        log.LogInformation("Migrations applied.");
    }
    catch (InvalidOperationException e)
    {
        log.LogInformation(e, "Missing database provider. Skipping database migration.");
    }
    catch (Exception e)
    {
        log.LogError(e, "HELP!!! I'm lost here");
    }
}

namespace Pest.Parcel
{
    public partial class Program
    {
        // needed for the sake of running pest.parcel.tests using TestWebApplicationFactory
    }
}