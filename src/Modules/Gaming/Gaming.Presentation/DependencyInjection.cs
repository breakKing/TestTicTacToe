using Gaming.Application;
using Gaming.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddGamingModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplication()
            .AddInfrastructure(configuration);
    }
}