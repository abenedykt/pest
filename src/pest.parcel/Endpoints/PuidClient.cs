namespace Pest.Parcel.Endpoints;

public class PuidClient(ILogger<PuidClient> logger) : IPuidClient
{
    
    public async Task<string> Fetch()
    {
        var c = new HttpClient();
        c.BaseAddress = new Uri("http://puid:8080");
        c.DefaultRequestHeaders.ConnectionClose = true;
        
        logger.LogInformation("Fetching puid from puid service");
        var result =  await c.GetStringAsync("/");
        logger.LogInformation("Fetched puid from puid service: {result}", result);
        return result;
    }
}