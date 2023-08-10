using Gaming.Domain.Common;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Games.DomainEvents;

public sealed record GameEndedDomainEvent(
    GameId GameId,
    DateTimeOffset FinishedAt,
    PlayerId? WinnerPlayerId,
    PlayerId? LoserPlayerId) : DomainEvent;