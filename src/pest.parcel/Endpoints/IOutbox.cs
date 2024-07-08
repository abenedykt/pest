using System.Text.Json;

namespace Pest.Parcel.Endpoints;

public interface IOutbox
{
    void Publish<T>(T message);
}

public class Outbox : IOutbox
{
    private readonly DataContext _db;

    public Outbox(DataContext dataContext)
    {
        _db = dataContext;
    }
    public void Publish<T>(T message)
    {
        var content = JsonSerializer.Serialize(message);

        _db.OutboxMessages.Add(new OutboxMessage
        {
            Content = content,
            CreatedAt = DateTime.UtcNow
        });

        _db.SaveChanges();
        // here we would publish the message to the message broker
        // for now, let's just print it to the console
        Console.WriteLine($"Message published: {content}");
    }
}