using System.Text.Json;

namespace Pest.Parcel.Endpoints;

public interface IOutbox
{
    void Publish<T>(T message);
}

public class Outbox : IOutbox
{
    private readonly OutboxDbContext _db;

    // public Outbox(DataContext dataContext)
    // {
    //     _db = dataContext;
    // }
    public void Publish<T>(T message)
    {
        var content = JsonSerializer.Serialize(message);
        var _db = new OutboxDbContext();
        _db.Messages.Add(new OutboxMessage
        {
              Data = content,
              CreatedAt = DateTime.UtcNow
        });
        //
         _db.SaveChanges();
        // here we would publish the message to the message broker
        // for now, let's just print it to the console
        Console.WriteLine($"Message published: {content}");
    }
}