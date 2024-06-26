using pest.tracking;
namespace pest.tests.e2e;

public class Tracing_WeatherForecastTests
{
    [Fact]
    public void WeatherForecast_returns_date_temperature_and_summary()
    {
        var temp = Random.Shared.Next(-20, 45);
        var text = "Short weather description for " + temp + " degrees Celsius?";
        var x = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), temp, text);
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), x.Date);
        Assert.Equal(temp, x.TemperatureC);
        Assert.Equal(text, x.Summary);
    }
    
    [Fact]
    public void Given_temperature_in_celsius_it_returns_temperature_in_fahrenheit()
    {
        var temp = Random.Shared.Next(-20, 45);
        var text = "Short weather description for " + temp + " degrees Celsius?";
        var x = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), temp, text);
        Assert.Equal(32 + (int)(temp / 0.5556), x.TemperatureF);
    }
}