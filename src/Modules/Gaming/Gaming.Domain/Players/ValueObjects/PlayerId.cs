using Common.Domain.Primitives;

namespace Gaming.Domain.Players.ValueObjects;

public sealed record PlayerId : ValueObject<Guid>
{
    /// <inheritdoc />
    private PlayerId(Guid value) : base(value)
    {
    }

    public static PlayerId CreateNew() => new(Guid.NewGuid());

    public static PlayerId CreateFromGuid(Guid value) => new(value);
}