namespace App.Configuration;

internal static class BuilderConfigurator
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure(builder.Configuration);
    }
}