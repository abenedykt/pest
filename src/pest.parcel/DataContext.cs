// using System.ComponentModel.DataAnnotations;
// using Microsoft.EntityFrameworkCore;
//
// namespace Pest.Parcel;
//
// public class DataContext(IConfiguration configuration) : DbContext
// {
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseNpgsql("host=localhost;database=pest;username=pest;password=pest");
//     }
//
//     private readonly IConfiguration _configuration = configuration;
//     
//     public DbSet<OutboxMessage> OutboxMessages { get; set; }
// }
//
//
// public class OutboxMessage
// {
//     [Key]
//     public int Id { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public string Content { get; set; }
// }



using Microsoft.EntityFrameworkCore;

public class OutboxDbContext : DbContext
{
    public DbSet<OutboxMessage> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=pest_parcel;Username=pest;Password=pest");
}

public class OutboxMessage
{
    public int Id { get; set; }
    public string Data { get; set; }
    public DateTime CreatedAt { get; set; }
}