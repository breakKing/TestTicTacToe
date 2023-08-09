using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Application.Games;

public interface IGameWriteRepository
{
    ValueTask<Game?> LoadAsync(GameId gameId, CancellationToken ct = default);

    void Add(Game game);

    void Update(Game game);
}