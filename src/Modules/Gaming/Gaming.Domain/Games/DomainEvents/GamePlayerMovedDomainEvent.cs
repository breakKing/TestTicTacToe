using Gaming.Domain.Common;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Games.DomainEvents;

public sealed record GamePlayerMovedDomainEvent(
    GameId GameId,
    PlayerId PlayerId,
    GameMoveId MoveId,
    FieldCoordinates Coordinates,
    DateTimeOffset MovedAt) : DomainEvent;