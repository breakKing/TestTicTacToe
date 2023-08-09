using Gaming.Application.Players;

namespace Gaming.Application.Lobbies;

public sealed record LobbyDto(
    Guid Id,
    PlayerDto InitiatorPlayer,
    PlayerDto? JoinedPlayer);