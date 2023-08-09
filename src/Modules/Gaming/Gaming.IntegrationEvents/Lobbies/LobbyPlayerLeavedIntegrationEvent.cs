namespace Gaming.IntegrationEvents.Lobbies;

public sealed record LobbyPlayerLeavedIntegrationEvent(
    Guid LobbyId,
    Guid PlayerId);