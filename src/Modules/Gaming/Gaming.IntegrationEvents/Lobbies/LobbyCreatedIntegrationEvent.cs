namespace Gaming.IntegrationEvents.Lobbies;

public sealed record LobbyCreatedIntegrationEvent(
    Guid LobbyId,
    Guid InitiatorPlayerId);