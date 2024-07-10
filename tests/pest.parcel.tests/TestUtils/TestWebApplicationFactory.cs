using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Pest.Parcel.Tests.TestUtils;

public class TestWebApplicationFactory<TProgram>() : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // todo: mock kafka
            // todo: mock repository
            
            
            
            
            
            // if (idGenerator == null) return;
            //
            // var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IIdGenerator<long>));
            // if (descriptor != null)
            // {
            //     services.Remove(descriptor);
            // }
            //     
            // services.AddSingleton(idGenerator);
        });

        return base.CreateHost(builder);
    }
}