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
            BootstrapServers = "kafka:9092" 
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
        var letter = _dbContext.Messages.FirstOrDefault();

        if (letter is not null)
        {
            _log.LogInformation("Found a letter in the outbox. Publishing to Kafka.");
            PublishToKafka(letter.Data);
            _dbContext.Messages.Remove(letter);
            var impacted = await _dbContext.SaveChangesAsync();
            _log.LogInformation("removed {impacted} letter from the outbox.",impacted); 
        }
        
    }

    private  void PublishToKafka(string letterData)
    {
        try
        {
            _producer.Produce(ParcelEventsTopic, new Message<Null, string>
            {
                Value = letterData
            });
        }
        catch (Exception e)
        {
            _log.LogDebug(e.Message);
        }
    }
}