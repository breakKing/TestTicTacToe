using Gaming.Domain.Common;
using Gaming.Domain.Lobbies.ValueObjects;

namespace Gaming.Domain.Lobbies.DomainEvents;

public sealed record LobbyDisbandedDomainEvent(LobbyId LobbyId) : DomainEvent;