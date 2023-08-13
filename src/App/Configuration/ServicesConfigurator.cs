using System.Text.Json;
using System.Text.Json.Serialization;
using App.Configuration.Messaging;
using FastEndpoints;
using FastEndpoints.Swagger;
using Gaming.Presentation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;

namespace App.Configuration;

internal static class ServicesConfigurator
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMainServices(configuration);
        services.AddGamingModule(configuration);
    }

    private static IServiceCollection AddMainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFastEndpoints();
        
        services.SwaggerDocument(o =>
        {
            o.EnableJWTBearerAuth = true;
            
            o.DocumentSettings = settings =>
            {
                settings.Title = "TestTicTacToe";
                settings.Version = "v1";
                settings.SchemaType = SchemaType.OpenApi3;
                settings.MarkNonNullablePropsAsRequired();
                settings.AllowNullableBodyParameters = true;
                settings.GenerateEnumMappingDescription = true;
                settings.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            };

            o.SerializerSettings = s =>
            {
                s.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                s.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                s.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            };

            o.ShortSchemaNames = true;
        });

        services.AddRabbit(configuration);
        
        return services;
    }
}