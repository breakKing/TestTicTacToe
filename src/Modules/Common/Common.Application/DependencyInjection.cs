using Common.Application.Pipeline;
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServicesForApplicationFromAssembly<TAssemblyMarker>(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<TAssemblyMarker>();
            options.Lifetime = ServiceLifetime.Scoped;

            options.AddOpenBehavior(typeof(ExceptionHandlerPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(UnitOfWorkPipelineBehavior<,>));

            options.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddValidatorsFromAssemblyContaining<TAssemblyMarker>(includeInternalTypes: true);

        return services;
    }
}