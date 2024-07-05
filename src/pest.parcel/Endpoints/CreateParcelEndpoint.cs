using FluentValidation;
using FluentValidation.Results;
using Pest.Parcel.Extenstions;

namespace Pest.Parcel.Endpoints;

public class CreateParcelEndpoint : IMinimalEndpoint
{
    private readonly Func<CreateParcelRequest?, IValidator<Sender>, IValidator<Recipient>, IOutbox, IResult> _createParcel =
        (request, sender, recipient, outbox) =>
        {
            if (request is not { Sender: not null, Recipient: not null })
                return Results.BadRequest("Missing sender or recipient");
            
            var validationResult = sender.Validate(request.Sender);
            
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
                
            validationResult = recipient.Validate(request.Recipient);
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            // here's a place for some logic
            // label creation or whatever :D
                
            outbox.Publish(request);
            return Results.Ok();
        };

    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/create", _createParcel).WithName("Create").WithOpenApi();
    }
}