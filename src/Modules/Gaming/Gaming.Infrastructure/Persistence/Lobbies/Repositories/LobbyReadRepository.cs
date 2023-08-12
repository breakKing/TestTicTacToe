using Gaming.Application.Common.Primitives.Pagination;
using Gaming.Application.Lobbies;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Infrastructure.Persistence.Lobbies.Repositories;

internal sealed class LobbyReadRepository : ILobbyReadRepository
{
    /// <inheritdoc />
    public async ValueTask<bool> IsPlayerInLobbyAsync(PlayerId playerId, CancellationToken ct = default)
    {
        return false;
    }

    /// <inheritdoc />
    public async ValueTask<LobbyDto?> GetPlayerLobbyAsync(PlayerId playerId, CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public async ValueTask<LobbyDto?> GetByIdAsync(LobbyId lobbyId, CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public async ValueTask<PagedList<LobbyDto>> GetAvailableLobbiesAsync(PlayerId playerId, PaginationRequest paginationRequest,
        CancellationToken ct = default)
    {
        return null;
    }
}