using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Players;

public interface IPlayerReadRepository
{
    ValueTask<bool> ExistsAsync(PlayerId id, CancellationToken ct = default);
}