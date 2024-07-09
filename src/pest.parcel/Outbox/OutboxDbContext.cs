using Microsoft.EntityFrameworkCore;

namespace Pest.Parcel.Outbox;

public class OutboxDbContext : DbContext
{
    public DbSet<OutboxMessage> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=postgres;Database=pest_parcel;Username=pest;Password=pest");
}