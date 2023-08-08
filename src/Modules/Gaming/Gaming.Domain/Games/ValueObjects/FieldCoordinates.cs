using Common.Domain.Primitives;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldCoordinates : ValueObject<(int X, int Y)>
{
    /// <inheritdoc />
    private FieldCoordinates((int X, int Y) value) : base(value)
    {
    }

    public static bool TryCreate(int x, int y, int maxCoordinate, out FieldCoordinates fieldCoordinates)
    {
        fieldCoordinates = new((0, 0));
        
        if (!Exists(x, y, maxCoordinate))
        {
            return false;
        }

        fieldCoordinates = new((x, y));

        return true;
    }

    private static bool Exists(int x, int y, int maxCoordinate)
    {
        return (x >= 0 && x < maxCoordinate) && (y >= 0 && y < maxCoordinate);
    }
}