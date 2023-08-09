using Gaming.Application.Players;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Application.Games;

public sealed record GameDto(
    Guid Id,
    PlayerDto FirstPlayer,
    PlayerDto SecondPlayer,
    FieldMark[][] Cells,
    Guid? LastMovePlayerId,
    DateTimeOffset StartedAt,
    DateTimeOffset? FinishedAt,
    GameResult Result);