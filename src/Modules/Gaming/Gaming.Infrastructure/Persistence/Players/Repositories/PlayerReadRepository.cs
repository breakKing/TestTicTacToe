using Gaming.Application.Players;
using Gaming.Domain.Players.Entities;
using Gaming.Domain.Players.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence.Players.Repositories;

internal sealed class PlayerReadRepository : IPlayerReadRepository
{
    private readonly GamingContext _context;

    public PlayerReadRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async ValueTask<bool> ExistsAsync(PlayerId id, CancellationToken ct = default)
    {
        return await _context.Set<Player>()
            .AnyAsync(p => p.Id == id, ct);
    }
}