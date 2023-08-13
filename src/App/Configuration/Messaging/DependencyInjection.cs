using MassTransit;

namespace App.Configuration.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConfig = configuration
            .GetRequiredSection(RabbitConfiguration.SectionName)
            .Get<RabbitConfiguration>()!;
        
        services.AddMassTransit(x =>
        {
            x.SetSnakeCaseEndpointNameFormatter();
            
            x.UsingRabbitMq((context,cfg) =>
            {
                cfg.Host(rabbitConfig.Host, "/", h => {
                    h.Username(rabbitConfig.Username);
                    h.Password(rabbitConfig.Password);
                    h.MaxMessageSize(uint.MaxValue);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}