using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldCoordinates : ValueObject<(int X, int Y)>
{
    public const int FieldSize = 3;
    
    /// <inheritdoc />
    private FieldCoordinates((int X, int Y) value) : base(value)
    {
    }

    public static bool TryCreate(int x, int y, out FieldCoordinates fieldCoordinates)
    {
        fieldCoordinates = new((0, 0));
        
        if (!Exists(x, y))
        {
            return false;
        }

        fieldCoordinates = new((x, y));

        return true;
    }

    private static bool Exists(int x, int y)
    {
        return (x >= 0 && x < FieldSize) && (y >= 0 && y < FieldSize);
    }
}