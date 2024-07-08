using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Pest.Parcel;

public class DataContext : DbContext
{
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("host=localhost;database=pest;username=pest;password=pest");
        
    }

    private readonly IConfiguration _configuration;
    
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}


public class OutboxMessage
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
}