using Identity.Core.Common.Identity;
using Identity.Core.Common.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity(configuration);
        services.AddJwtAuth(configuration);

        return services;
    }
}