using Common.Domain.Primitives;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Lobbies.DomainEvents;

public sealed record LobbyPlayerLeavedDomainEvent(
    LobbyId LobbyId,
    PlayerId PlayerId) : DomainEvent;