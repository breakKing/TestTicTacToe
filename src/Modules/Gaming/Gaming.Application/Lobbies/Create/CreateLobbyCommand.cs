using Gaming.Application.Common.Handling;

namespace Gaming.Application.Lobbies.Create;

public sealed record CreateLobbyCommand(Guid PlayerId) : ICommand<Guid>;