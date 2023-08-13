using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace App.Configuration;

internal static class AppPipelineConfigurator
{
    public static void Configure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseFastEndpoints(config =>
        {
            config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.Endpoints.RoutePrefix = "api";
        });
        
        app.UseSwaggerGen();
    }
}