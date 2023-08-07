using Common.Domain.Primitives;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameResult : ValueObject<int>
{
    /// <inheritdoc />
    private GameResult(int value) : base(value)
    {
    }
    
    public static GameResult StillInProgress => new(0);
    public static GameResult Draw => new(1);
    public static GameResult FirstPlayerVictory => new(2);
    public static GameResult SecondPlayerVictory => new(3);
}