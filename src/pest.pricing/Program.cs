using Microsoft.SemanticKernel;
using Kernel = Microsoft.SemanticKernel.Kernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Codeblaze.SemanticKernel.Connectors.Ollama;

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

// var kernelBuilder = Kernel.CreateBuilder();

var modelId = "phi3";
var ollamaUri = new Uri("http://localhost:11434");

// builder.Services.AddOpenAIChatCompletion(modelId, ollamaUri, apiKey: null);
// kernelBuilder.AddOllamaTextGeneration(modelId, ollamaUri);


var builder = WebApplication.CreateBuilder(args);

// lets some AI Kernel
var kernel = builder.Services.AddKernel();
kernel.AddOpenAIChatCompletion(modelId, ollamaUri, apiKey: null);


// builder.Services.AddTransient<HttpClient>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
