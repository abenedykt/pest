using Pest.Parcel.Extenstions;

namespace Pest.Parcel.Endpoints;

public class CreateParcelEndpoint : IMinimalEndpoint
{
    private readonly Func<CreateParcelRequest?,IResult> _createParcel = request =>
    {
        if(request is { Sender: not null, Recipient: not null })
        {
            return Results.Ok();
        }

        return Results.BadRequest("missing stuff");
    };
    
    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        builder
            .MapPost("/create", _createParcel)
            .WithName("Create")
            .WithOpenApi();
    }
}