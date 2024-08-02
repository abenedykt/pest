using Confluent.Kafka;

namespace Pest.Parcel.Outbox;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _log;
    private readonly IOutboxRepository _outboxRepository;

    private readonly IProducer<Null,string> _kafkaProducer;

    private const string ParcelEventsTopic = "parcel_events";
    
    public Worker(ILogger<Worker> log, IOutboxRepository outboxRepository)
    {
        _log = log;
        _outboxRepository = outboxRepository;
        
        _kafkaProducer = CreateKafkaProducer();
    }

    private IProducer<Null, string> CreateKafkaProducer()
    {
        var config = new ProducerConfig { 
            BootstrapServers = "kafka:9092" 
        };

        return  new ProducerBuilder<Null, string>(config)
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
        var letter = await _outboxRepository.GetFirstMessage();

        if (letter is not null)
        {
            _log.LogInformation("Found a letter in the outbox. Publishing to Kafka.");
            PublishToKafka(letter.Data);
            await _outboxRepository.Remove(letter);
            _log.LogInformation("removed {letter} letter from the outbox.",letter.Id); 
        }
        
    }

    private  void PublishToKafka(string letterData)
    {
        try
        {
            _kafkaProducer.Produce(ParcelEventsTopic, new Message<Null, string>
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