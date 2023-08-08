using Common.Domain.Primitives;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameMoveId : ValueObject<Guid>
{
    /// <inheritdoc />
    private GameMoveId(Guid value) : base(value)
    {
    }

    public static GameMoveId Create() => new(Guid.NewGuid());
}