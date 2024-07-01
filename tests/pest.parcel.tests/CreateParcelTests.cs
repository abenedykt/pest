using System.Net;
using FluentAssertions;
using Pest.Parcel.Tests.TestUtils;

namespace Pest.Parcel.Tests;

public class CreateParcelTests
{
    private readonly HttpClient _client;

    public CreateParcelTests()
    {
        var app =  new TestWebApplicationFactory<Program>();
        _client = app.CreateClient();
    }
    [Fact]
    public async Task Create_parcel_returns_200OK()
    {
        var result = await _client.PostAsync("/create", new StringContent(""));

        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}