namespace Gaming.IntegrationEvents.Lobbies;

public sealed record LobbyPlayerJoinedIntegrationEvent(
    Guid LobbyId,
    Guid PlayerId);