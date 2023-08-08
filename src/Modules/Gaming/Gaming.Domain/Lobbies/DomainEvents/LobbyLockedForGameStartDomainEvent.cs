using Common.Domain.Primitives;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Lobbies.DomainEvents;

public sealed record LobbyLockedForGameStartDomainEvent(
    LobbyId LobbyId,
    PlayerId FirstPlayerId,
    PlayerId SecondPlayerId,
    DateTimeOffset GameStartedAt) : DomainEvent;