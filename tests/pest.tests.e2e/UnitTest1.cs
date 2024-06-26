using System.Diagnostics;
using DotNet.Testcontainers.Builders;
using pest.examples;

namespace pest.tests.e2e;

public class UnitTest1
{
    [Fact]
    public async Task Spin_up_example_test_container()
    {
        // Create a new instance of a container.
        var container = new ContainerBuilder()
          // Set the image for the container to "testcontainers/helloworld:1.1.0".
          .WithImage("testcontainers/helloworld:1.1.0")
          // Bind port 8080 of the container to a random port on the host.
          .WithPortBinding(8080, true)
          // Wait until the HTTP endpoint of the container is available.
          .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
          // Build the container configuration.
          .Build();

        // Start the container.
        await container.StartAsync()
          .ConfigureAwait(false);

        // Create a new instance of HttpClient to send HTTP requests.
        var httpClient = new HttpClient();

        // Construct the request URI by specifying the scheme, hostname, assigned random host port, and the endpoint "uuid".
        var requestUri = new UriBuilder(Uri.UriSchemeHttp, container.Hostname, container.GetMappedPublicPort(8080), "uuid").Uri;

        // Send an HTTP GET request to the specified URI and retrieve the response as a string.
        var guid = await httpClient.GetStringAsync(requestUri)
          .ConfigureAwait(false);

        // Ensure that the retrieved UUID is a valid GUID.
        Debug.Assert(Guid.TryParse(guid, out _));

        Assert.NotNull(guid);
    }

    [Fact]
    public void Spin_up_testcontainer_with_kafka(){
        
    }

        [Fact]
    public void Test1()
    {
        var x = new Person("Miguel", "Camba");
        Assert.Equal("Miguel", x.Name);
        Assert.Equal("Camba", x.Surname);
    }

    [Fact]
    public void ToString_returns_name_and_surname()
    {
        var x = new Person("Miguel", "Camba");
        Assert.Equal("Miguel Camba", x.ToString());
    }
}