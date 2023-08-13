using System.IdentityModel.Tokens.Jwt;
using FastEndpoints.Security;
using Identity.Core.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Core.Common.Jwt;

internal static class DependencyInjection
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration
            .GetRequiredSection(JwtConfiguration.SectionName)
            .Get<JwtConfiguration>()!;

        services.AddOptions<JwtConfiguration>()
            .BindConfiguration(JwtConfiguration.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddJWTBearerAuth(jwtConfig.TokenSigningKey);

        services.AddTransient<TokenService>();

        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        
        return services;
    }
}