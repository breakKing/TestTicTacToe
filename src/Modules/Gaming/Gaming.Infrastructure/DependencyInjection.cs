using Gaming.Infrastructure.Messaging;
using Gaming.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayerForGaming(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEntityFrameworkCore(configuration);
        services.AddRabbit(configuration);
        
        return services;
    }
}