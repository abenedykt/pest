using IdGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/", () => new IdGenerator(0).CreateId()).WithOpenApi();

app.Run();