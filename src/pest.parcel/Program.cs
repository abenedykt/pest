using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Pest.Parcel.Endpoints;
using Pest.Parcel.Extenstions;
using Pest.Parcel.Outbox;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddMinimalEndpoints();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddTransient<IOutbox, Outbox>();
builder.Services.AddTransient<IPuidClient, PuidClient>();
builder.Services.AddDbContext<OutboxDbContext>();

builder.Services.AddHostedService<Worker>(); // should be out of process. We want to run app and outbox separately


var app = builder.Build();

// Configure the HTTP request pipeline.
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

    var context = services.GetRequiredService<OutboxDbContext>();
    var log = services.GetRequiredService<ILogger<Program>>();
    
    log.LogInformation("Looking for pending database migrations.");
    if (!context.Database.GetPendingMigrations().Any()) return;
        
    log.LogInformation("Found! Applying...");
    context.Database.Migrate();
    log.LogInformation("Migrations applied.");
}

namespace Pest.Parcel
{
    public partial class Program
    {
        // needed for the sake of running pest.parcel.tests using TestWebApplicationFactory
    }
}