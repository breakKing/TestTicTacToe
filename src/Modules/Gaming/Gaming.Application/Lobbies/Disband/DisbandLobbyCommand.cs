using Common.Application.Handling;

namespace Gaming.Application.Lobbies.Disband;

public sealed record DisbandLobbyCommand(
    Guid LobbyId,
    Guid PlayerId) : ICommand;