using Gaming.Application.Games;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Infrastructure.Persistence.Games.Repositories;

internal sealed class GameWriteRepository : IGameWriteRepository
{
    /// <inheritdoc />
    public async ValueTask<Game?> LoadAsync(GameId gameId, CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public void Add(Game game)
    {
    }

    /// <inheritdoc />
    public void Update(Game game)
    {
    }
}