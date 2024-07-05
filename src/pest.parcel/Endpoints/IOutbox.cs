using System.Text.Json;

namespace Pest.Parcel.Endpoints;

public interface IOutbox
{
    void Publish<T>(T message);
}

public class Outbox : IOutbox
{

    public Outbox()
    {
        
    }
    public void Publish<T>(T message)
    {
        var content = JsonSerializer.Serialize(message);   
        // here we would publish the message to the message broker
        // for now, let's just print it to the console
        Console.WriteLine($"Message published: {content}");
    }
}