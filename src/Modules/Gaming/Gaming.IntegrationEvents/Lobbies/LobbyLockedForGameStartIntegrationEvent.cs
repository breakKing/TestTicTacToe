namespace Gaming.IntegrationEvents.Lobbies;

public sealed record LobbyLockedForGameStartIntegrationEvent(
    Guid LobbyId,
    Guid FirstPlayerId,
    Guid SecondPlayerId);