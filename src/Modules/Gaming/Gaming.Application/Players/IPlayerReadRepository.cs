using Gaming.Domain.Players.Entities;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Players;

public interface IPlayerReadRepository
{
    ValueTask<Player?> GetByIdAsync(PlayerId id, CancellationToken ct = default);
}