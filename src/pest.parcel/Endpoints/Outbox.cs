using System.Text.Json;

namespace Pest.Parcel.Endpoints;

public class Outbox(OutboxDbContext dbContext) : IOutbox
{
    public void Publish<T>(T message)
    {
        var content = JsonSerializer.Serialize(message);

        using (var transaction = dbContext.Database.BeginTransaction())
        {
            dbContext.Messages.Add(new OutboxMessage
            {
                Data = content,
                CreatedAt = DateTime.UtcNow
            });
            dbContext.SaveChanges();
            
            transaction.Commit();    
        }
        
        Console.WriteLine($"Message added to outbox: {content}");
    }
}