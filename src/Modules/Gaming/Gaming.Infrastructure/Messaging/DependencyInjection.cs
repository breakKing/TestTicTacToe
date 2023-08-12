using Gaming.Application.Common.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConfig = configuration
            .GetRequiredSection(RabbitConfiguration.SectionName)
            .Get<RabbitConfiguration>()!;
        
        services.AddMassTransit(x =>
        {
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

        services.AddScoped<IMessageBus, RabbitMessageBus>();
        
        return services;
    }
}