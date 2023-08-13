using Gaming.Application.Games;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence.Games.Repositories;

internal sealed class GameWriteRepository : IGameWriteRepository
{
    private readonly GamingContext _context;

    public GameWriteRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Game?> LoadAsync(GameId gameId, CancellationToken ct = default)
    {
        return await _context.Set<Game>()
            .SingleOrDefaultAsync(g => g.Id == gameId, ct);
    }

    /// <inheritdoc />
    public void Add(Game game)
    {
        _context.Add(game);
    }

    /// <inheritdoc />
    public void Update(Game game)
    {
        _context.Update(game);
    }
}