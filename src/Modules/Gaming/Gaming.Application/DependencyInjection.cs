using FluentValidation;
using Gaming.Application.Common.Pipeline;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            options.Lifetime = ServiceLifetime.Scoped;

            options.AddOpenBehavior(typeof(ExceptionHandlerPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(UnitOfWorkPipelineBehavior<,>));

            options.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>(includeInternalTypes: true);

        return services;
    }
}