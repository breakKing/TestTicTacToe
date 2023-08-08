using Common.Domain.Primitives;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameId : ValueObject<Guid>
{
    /// <inheritdoc />
    private GameId(Guid value) : base(value)
    {
    }

    public static GameId CreateNew() => new(Guid.NewGuid());
}