using FastEndpoints;
using FastEndpoints.Swagger;

namespace App.Configuration;

internal static class AppPipelineConfigurator
{
    public static void Configure(this WebApplication app)
    {
        app.UseAuthorization();
        app.UseFastEndpoints();
        app.UseSwaggerGen();
    }
}