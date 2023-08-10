using Gaming.Application.Common.Handling;

namespace Gaming.Application.Games.Move;

public sealed record PlayerMoveCommand(
    Guid PlayerId,
    Guid GameId,
    int X,
    int Y) : ICommand;