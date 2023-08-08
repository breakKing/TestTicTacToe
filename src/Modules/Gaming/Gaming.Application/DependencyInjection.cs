using Common.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayerForGaming(this IServiceCollection services)
    {
        services.AddMainServicesForApplicationFromAssembly<IAssemblyMarker>();

        return services;
    }
}