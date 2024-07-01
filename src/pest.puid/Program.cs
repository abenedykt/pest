using IdGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIdGenerator<long>>(new IdGenerator(0));
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/", (IIdGenerator<long> g) => g.CreateId()).WithOpenApi();

app.Run();
    
public partial class Program {
    // needed for the sake of running pest.puid.tests using TestWebApplicationFactory
}