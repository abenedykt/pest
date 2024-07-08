using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pest.Parcel.Endpoints;
using Pest.Parcel.Extenstions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddMinimalEndpoints();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient<IOutbox, Outbox>();
builder.Services.AddDbContext<OutboxDbContext>();

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
app.Run();

namespace Pest.Parcel
{
    public partial class Program
    {
        // needed for the sake of running pest.parcel.tests using TestWebApplicationFactory
    }
}