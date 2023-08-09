namespace Gaming.IntegrationEvents.Games;

public sealed record GamePlayerMovedIntegrationEvent(
    Guid GameId,
    Guid PlayerId,
    int X,
    int Y,
    DateTimeOffset MovedAt);