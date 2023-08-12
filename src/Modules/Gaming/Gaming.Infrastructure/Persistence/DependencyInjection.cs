using Gaming.Application.Common.Persistence;
using Gaming.Application.Games;
using Gaming.Application.Lobbies;
using Gaming.Application.Players;
using Gaming.Infrastructure.Persistence.Games.Repositories;
using Gaming.Infrastructure.Persistence.Lobbies.Repositories;
using Gaming.Infrastructure.Persistence.Players.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GamingContext>((serviceProvider, ctxOptions) =>
        {
            ctxOptions.UseNpgsql(
                configuration.GetConnectionString("GamingDatabase"),
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

        services.AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IGameReadRepository, GameReadRepository>()
            .AddScoped<IGameWriteRepository, GameWriteRepository>()
            .AddScoped<ILobbyReadRepository, LobbyReadRepository>()
            .AddScoped<ILobbyWriteRepository, LobbyWriteRepository>()
            .AddScoped<IPlayerReadRepository, PlayerReadRepository>()
            .AddScoped<IPlayerWriteRepository, PlayerWriteRepository>();
    }
}