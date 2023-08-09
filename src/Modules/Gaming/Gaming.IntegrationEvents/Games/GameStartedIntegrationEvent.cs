namespace Gaming.IntegrationEvents.Games;

public sealed record GameStartedIntegrationEvent(
    Guid GameId,
    Guid FirstPlayerId,
    Guid SecondPlayerId,
    DateTimeOffset GameStartedAt);