using Gaming.Application.Common.Handling;

namespace Gaming.Application.Lobbies.Lock;

public sealed record LobbyLockCommand(
    Guid PlayerId,
    Guid LobbyId) : ICommand;