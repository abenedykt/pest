using FluentValidation;
using Pest.Parcel.Extenstions;

namespace Pest.Parcel.Endpoints;

public class CreateParcelEndpoint(ILogger<CreateParcelEndpoint> logger) : IMinimalEndpoint
{
    private readonly Func<CreateParcelRequest?, IValidator<Sender>, IValidator<Recipient>, IPuidClient, IOutbox, Task<IResult>> _createParcel = async (request, sender, recipient, puidClient, outbox) =>
        {
            logger.LogInformation("Creating parcel");

            if (request is not { Sender: not null, Recipient: not null })
            {
                logger.LogError("Cannot create parcel with request: {request}", request);
                return Results.BadRequest("Missing sender or recipient");
            }
                
            
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