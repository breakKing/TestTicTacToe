using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameResult : ValueObject<int>
{
    /// <inheritdoc />
    private GameResult(int value) : base(value)
    {
    }

    public static GameResult CreateFromValue(int value) => new(value);
    
    public static GameResult StillInProgress => new(0);
    public static GameResult FirstPlayerVictory => new(1);
    public static GameResult SecondPlayerVictory => new(2);
    public static GameResult Draw => new(3);
}