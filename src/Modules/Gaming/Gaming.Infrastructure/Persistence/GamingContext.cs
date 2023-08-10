using Gaming.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence;

internal sealed class GamingContext : DbContext, IUnitOfWork
{
    /// <inheritdoc />
    public GamingContext(DbContextOptions options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamingContext).Assembly);
    }
}