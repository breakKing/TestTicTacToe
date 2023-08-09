using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkCore<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string dbConnectionStringName = "Database")
        where TContext : DbContext, IUnitOfWork
    {
        services.AddDbContext<TContext>(ctxOptions =>
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
        });

        services.AddScoped(typeof(IUnitOfWork), serviceProvider =>
        {
            var context = serviceProvider.GetRequiredService<TContext>();
            return context;
        });
        
        return services;
    }
}