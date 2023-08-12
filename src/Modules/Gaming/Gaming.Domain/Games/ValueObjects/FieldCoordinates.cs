using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldCoordinates : ValueObject<int, int>
{
    public const int FieldSize = 3;
    
    public int X => Value1;
    public int Y => Value2;
    
    /// <inheritdoc />
    private FieldCoordinates(int value1, int value2) : base(value1, value2)
    {
    }

    public static bool TryCreate(int x, int y, out FieldCoordinates fieldCoordinates)
    {
        fieldCoordinates = new(0, 0);
        
        if (!Exists(x, y))
        {
            return false;
        }

        fieldCoordinates = new(x, y);

        return true;
    }

    private static bool Exists(int x, int y)
    {
        return (x is >= 0 and < FieldSize) && (y is >= 0 and < FieldSize);
    }
}