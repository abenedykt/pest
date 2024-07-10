using FluentValidation;
using Pest.Parcel.Extenstions;

namespace Pest.Parcel.Endpoints;

public class CreateParcelEndpoint : IMinimalEndpoint
{
    private readonly Func<CreateParcelRequest?, IValidator<Sender>, IValidator<Recipient>, IPuidClient, IOutbox, Task<IResult>> _createParcel = async (request, sender, recipient, puidClient, outbox) =>
        {
            if (request is not { Sender: not null, Recipient: not null })
                return Results.BadRequest("Missing sender or recipient");
            
            var validationResult = sender.Validate(request.Sender);
            
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
                
            validationResult = recipient.Validate(request.Recipient);
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            // here's a place for some logic
            // add parcel number
            // publish parcel created event
            var parcelNumber = await puidClient.Fetch();
            var parcelCreatedEvent = new ParcelCreatedEvent
            {
                ID = parcelNumber,
                Sender = request.Sender,
                Recipient = request.Recipient,
                CreatedAt = DateTime.UtcNow,
                Class = request.Class
            };
                
            outbox.Publish(parcelCreatedEvent);
            return Results.Ok(parcelNumber);
        };

    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/create", _createParcel).WithName("Create").WithOpenApi();
    }
}

public class PuidClient(ILogger<PuidClient> _logger) : IPuidClient
{
    
    public async Task<string> Fetch()
    {
        var c = new HttpClient();
        c.BaseAddress = new Uri("http://puid:8080");
        c.DefaultRequestHeaders.ConnectionClose = true;
        
        _logger.LogInformation("Fetching puid from puid service");
        var result =  await c.GetStringAsync("/");
        _logger.LogInformation("Fetched puid from puid service: {result}", result);
        return result;
    }
}

public interface IPuidClient
{
    Task<string> Fetch();
}

internal class ParcelCreatedEvent : DomainEvent
{
    public string ID { get; set; }

    public override string EventType => "ParcelCreated";
    public Sender Sender { get; set; }
    public Recipient Recipient { get; set; }
    public ParcelClass Class { get; set; }
}

internal abstract class DomainEvent
{
    public abstract string EventType { get; }
    public DateTime CreatedAt { get; set; }
}