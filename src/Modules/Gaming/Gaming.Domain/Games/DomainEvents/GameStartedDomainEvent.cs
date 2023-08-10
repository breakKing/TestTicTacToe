using Gaming.Domain.Common;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Games.DomainEvents;

public sealed record GameStartedDomainEvent(
    GameId GameId,
    PlayerId FirstPlayerId,
    PlayerId SecondPlayerId,
    DateTimeOffset StartedAt) : DomainEvent;