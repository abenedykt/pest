using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pest.Parcel.Extenstions;

public static class MinimalEndpointsExtensions
{
    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
        var endpoints = 
            assembly.DefinedTypes
                .Where(t => !t.IsAbstract 
                            && !t.IsInterface 
                            && t.IsAssignableTo(typeof(IMinimalEndpoint)))
                .Select((t => ServiceDescriptor.Transient(typeof(IMinimalEndpoint), t)));
            
        services.TryAddEnumerable(endpoints);
        return services;
    }

    public static IApplicationBuilder RegisterMinimalEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IMinimalEndpoint>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapRoutes(app);
        }

        return app;
    }
}