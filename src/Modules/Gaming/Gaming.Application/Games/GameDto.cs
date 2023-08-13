using Gaming.Application.Players;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Application.Games;

public sealed class GameDto
{
    public required Guid Id { get; init; }
    public required PlayerDto FirstPlayer { get; init; }
    public required PlayerDto SecondPlayer { get; init; }
    public required IReadOnlyList<IReadOnlyList<FieldMark>> Field { get; init; }
    public Guid? LastMovePlayerId { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
    public DateTimeOffset? FinishedAt { get; init; }
    public required GameResult Result { get; init; }
}