using Microsoft.EntityFrameworkCore;

namespace Pest.Parcel.Outbox;

public class OutboxRepository : IOutboxRepository
{
    private readonly OutboxDbContext _dbContext;

    public OutboxRepository()
    {
        _dbContext = new OutboxDbContext();
    }
    public async Task<OutboxMessage?> GetFirstMessage()
    {
        return await _dbContext.Messages.FirstOrDefaultAsync();
    }

    public async Task Remove(OutboxMessage letterToRemove)
    {
         _dbContext.Messages.Remove(letterToRemove);
         await _dbContext.SaveChangesAsync();
    }
}

public interface IOutboxRepository
{
    Task<OutboxMessage?> GetFirstMessage();
    Task Remove(OutboxMessage letterToRemove);
}