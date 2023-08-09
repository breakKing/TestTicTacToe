using Gaming.Application.Players;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Application.Games;

public sealed record GameDto(
    Guid Id,
    PlayerDto FirstPlayer,
    PlayerDto SecondPlayer,
    int[][] CellValues,
    Guid? LastMovePlayerId,
    DateTimeOffset StartedAt,
    DateTimeOffset? FinishedAt,
    GameResult Result);