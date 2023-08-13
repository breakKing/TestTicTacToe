using Gaming.Application.Common.Handling;

namespace Gaming.Application.Games.RandomMove;

public sealed record RandomMoveAfterTimeoutCommand(Guid GameId, Guid PlayerId, Guid? LastMoveId) : ICommand;