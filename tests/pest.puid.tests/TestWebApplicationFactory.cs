using IdGen;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace pest.puid.tests;

public class TestWebApplicationFactory<TProgram>(IIdGenerator<long>? idGenerator) : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            if (idGenerator == null) return;
            
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IIdGenerator<long>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
                
            services.AddSingleton(idGenerator);
        });

        return base.CreateHost(builder);
    }
}