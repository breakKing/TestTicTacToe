namespace Gaming.IntegrationEvents.Games;

public sealed record GameEndedIntegrationEvent(
    Guid GameId,
    DateTimeOffset FinishedAt,
    Guid? WinnerPlayerId,
    Guid? LoserPlayerId);