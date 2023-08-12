using Gaming.Application.Players;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Infrastructure.Persistence.Players.Repositories;

internal sealed class PlayerReadRepository : IPlayerReadRepository
{
    /// <inheritdoc />
    public async ValueTask<bool> ExistsAsync(PlayerId id, CancellationToken ct = default)
    {
        return false;
    }
}