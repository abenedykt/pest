using System.Net;
using AutoFixture.Xunit2;
using FluentAssertions;
using IdGen;
using NSubstitute;

namespace pest.puid.tests;

public class PestPuidWebServiceTests
{
    [Theory, AutoData]
    public async void Get_returns_newly_generated_identifier(long testIdentifier)
    {
        var app = CreateMockedApp(testIdentifier);
        var client = app.CreateClient();
        
        var result = await client.GetAsync("/");
        var content = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Be(testIdentifier.ToString());
    }

    private static TestWebApplicationFactory<Program> CreateMockedApp(long testId)
    {
        var mockGenerator = Substitute.For<IIdGenerator<long>>();
        mockGenerator.CreateId().Returns(x => testId);
        
        return new TestWebApplicationFactory<Program>(mockGenerator);
    }
}