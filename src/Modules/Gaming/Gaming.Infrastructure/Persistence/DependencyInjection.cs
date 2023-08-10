using Gaming.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkCore(
        this IServiceCollection services,
        IConfiguration configuration,
        string dbConnectionStringName = "Database")
    {
        services.AddDbContext<GamingContext>((serviceProvider, ctxOptions) =>
        {
            ctxOptions.UseNpgsql(
                configuration.GetConnectionString(dbConnectionStringName),
                npgsql =>
                {
                    npgsql.CommandTimeout(30);
                    npgsql.EnableRetryOnFailure(3);
                    npgsql.MigrationsHistoryTable("migrations", "Maintenance");
                    npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            ctxOptions.UseSnakeCaseNamingConvention();

            ctxOptions.AddInterceptors(
                new DomainEventPublisherInterceptor(serviceProvider.GetRequiredService<IPublisher>()));
        });

        services.AddScoped<IUnitOfWork>(serviceProvider =>
        {
            var context = serviceProvider.GetRequiredService<GamingContext>();
            return context;
        });
        
        return services;
    }
}