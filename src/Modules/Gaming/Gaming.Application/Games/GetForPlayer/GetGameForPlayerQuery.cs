using Gaming.Application.Common.Handling;

namespace Gaming.Application.Games.GetForPlayer;

public sealed record GetGameForPlayerQuery(
    Guid GameId,
    Guid PlayerId) : IQuery<GameDto>;