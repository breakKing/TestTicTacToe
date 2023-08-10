using Gaming.Application.Common.Handling;

namespace Gaming.Application.Lobbies.Join;

public sealed record JoinLobbyCommand(Guid PlayerId, Guid LobbyId) : ICommand;