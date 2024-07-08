using Confluent.Kafka;

namespace Pest.Parcel.Outbox;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _log;
    private readonly OutboxDbContext _dbContext;
    private readonly IProducer<Null,string> _producer;

    
    private const string ParcelEventsTopic = "parcel_events";
    
    public Worker(ILogger<Worker> log, OutboxDbContext dbContext)
    {
        _log = log;
        _dbContext = dbContext;
        
        
        var config = new ProducerConfig { 
            BootstrapServers = "localhost:9092" 
        };
        
        _producer = new ProducerBuilder<Null, string>(config)
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _log.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await ProcessOutbox();
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task ProcessOutbox()
    {
        var letters = _dbContext.Messages.Take(100);

        foreach (var letter in letters)
        {
            // lets stay super optimistic for the sake of readability
            
            await PublishToKafka(letter.Data);
            _dbContext.Messages.Remove(letter);
           
        }
        
        await _dbContext.SaveChangesAsync(); // <- efficiency vs idempotency (what about worker scaling)
    }

    private async Task PublishToKafka(string letterData)
    {
        await _producer.ProduceAsync(ParcelEventsTopic, new Message<Null, string>
        {
            Value = letterData
        });
    }
}