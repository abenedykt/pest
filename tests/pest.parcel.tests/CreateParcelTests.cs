using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Pest.Parcel.Endpoints;
using Pest.Parcel.Tests.TestUtils;

namespace Pest.Parcel.Tests;

public class CreateParcelTests
{
    private readonly HttpClient _client;

    public CreateParcelTests()
    {
        // mock puid client
        var app =  new TestWebApplicationFactory<Program>();
        _client = app.CreateClient();
    }
    
    [Fact]
    public async Task Create_parcel_returns_200OK()
    {
        var parcel = CreateExampleParcel();
        var result = await _client.PostAsJsonAsync("/create", parcel);
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        
        // validate package request
        // - parcel has sender
        // - parcel has recipient
        // - parcel has dimensions (width, height, length)
        
        // fetch parcel id from puid service
        
        // send parcel information to kafka
        
        // return parcel id 
        
        // at this point tracking should recognize parcel
        // at this point you can retrieve parcel label (?) 
        
        
    }

    private static CreateParcelRequest CreateExampleParcel()
    {
        return new CreateParcelRequest
        {
            Sender = new Sender
            {
                Name = "John Doe",
                Address = "123 Main",
                City = "Springfield",
                Country = "USA",
                PostalCode = "12345"
            },
            Recipient = new Recipient
            {
                Name = "Jane Doe",
                Address = "456 Main",
                City = "Springfield",
                Country = "USA",
                PostalCode = "12345"
            },
            Class = ParcelClass.Standard
        };
    }
}

    