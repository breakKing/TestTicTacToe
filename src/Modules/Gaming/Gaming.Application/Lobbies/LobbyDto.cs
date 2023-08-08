namespace Gaming.Application.Lobbies;

public sealed record LobbyDto(
    Guid LobbyId,
    Guid InitiatorPlayerId,
    Guid? JoinedPlayerId);