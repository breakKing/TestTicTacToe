using Gaming.Application.Common.Primitives.Pagination;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies;

public interface ILobbyReadRepository
{
    ValueTask<bool> IsPlayerInLobbyAsync(PlayerId playerId, CancellationToken ct = default);
    
    ValueTask<LobbyDto?> GetPlayerLobbyAsync(PlayerId playerId, CancellationToken ct = default);
    
    ValueTask<PagedList<LobbyDto>> GetAvailableLobbiesAsync(
        PlayerId playerId, 
        PaginationRequest paginationRequest,
        CancellationToken ct = default);
}