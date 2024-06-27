using System.Net;
using FluentAssertions;

namespace pest.puid.tests;

public class PestPuidWebServiceTests
{
    [Fact]
    public async void Simple_get_should_return_not_null_identifier()
    {
        var factory = new TestWebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var result = await client.GetAsync("/");
        var content = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNullOrWhiteSpace();
    }

}