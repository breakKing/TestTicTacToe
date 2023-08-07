using Common.Domain.Primitives;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldId : ValueObject<Guid>
{
    /// <inheritdoc />
    private FieldId(Guid value) : base(value)
    {
    }
    
    public static FieldId Create() => new(Guid.NewGuid());
}