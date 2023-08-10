using Gaming.Domain.Common;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Lobbies.DomainEvents;

public sealed record LobbyPlayerJoinedDomainEvent(
    LobbyId LobbyId,
    PlayerId PlayerId) : DomainEvent;