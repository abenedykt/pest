using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Pest.Parcel.Tests.TestUtils;

public class TestWebApplicationFactory<TProgram>() : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }
}