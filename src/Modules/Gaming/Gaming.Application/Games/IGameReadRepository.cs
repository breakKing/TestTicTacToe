using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Application.Games;

public interface IGameReadRepository
{
    ValueTask<GameDto?> GetByIdAsync(GameId gameId, CancellationToken ct = default);
}