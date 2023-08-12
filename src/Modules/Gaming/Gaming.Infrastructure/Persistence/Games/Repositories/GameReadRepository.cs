using Gaming.Application.Games;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Infrastructure.Persistence.Games.Repositories;

internal sealed class GameReadRepository : IGameReadRepository
{
    /// <inheritdoc />
    public async ValueTask<GameDto?> GetByIdAsync(GameId gameId, CancellationToken ct = default)
    {
        return null;
    }
}