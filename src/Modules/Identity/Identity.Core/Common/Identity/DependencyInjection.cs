using Identity.Core.Common.Identity.Entites;
using Identity.Core.Common.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Core.Common.Identity;

internal static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(ctxOptions =>
        {
            ctxOptions.UseNpgsql(
                configuration.GetConnectionString("IdentityDatabase"),
                npgsql =>
                {
                    npgsql.CommandTimeout(30);
                    npgsql.EnableRetryOnFailure(3);
                    npgsql.MigrationsHistoryTable("migrations", "Maintenance");
                    npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            ctxOptions.UseSnakeCaseNamingConvention();
        });

        services.AddIdentity<User, Role>(
                opt =>
                {
                    opt.SignIn.RequireConfirmedAccount = false;
                    opt.SignIn.RequireConfirmedEmail = false;
                    opt.SignIn.RequireConfirmedPhoneNumber = false;

                    opt.Password.RequireDigit = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequiredUniqueChars = 1;

                    opt.Lockout.AllowedForNewUsers = false;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
                    opt.Lockout.MaxFailedAccessAttempts = 1000;

                    opt.User.RequireUniqueEmail = false;
                })
            .AddEntityFrameworkStores<IdentityContext>();
        
        return services;
    }
}