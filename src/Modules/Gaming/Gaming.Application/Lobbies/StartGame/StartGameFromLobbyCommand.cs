using Common.Application.Handling;

namespace Gaming.Application.Lobbies.StartGame;

public sealed record StartGameFromLobbyCommand(
    Guid PlayerId,
    Guid LobbyId) : ICommand;