using Microsoft.EntityFrameworkCore;
using Pest.Parcel;

public class OutboxDbContext : DbContext
{
    public DbSet<OutboxMessage> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=pest_parcel;Username=pest;Password=pest");
}