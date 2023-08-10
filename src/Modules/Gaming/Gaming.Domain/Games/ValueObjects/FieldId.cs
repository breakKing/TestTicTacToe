using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldId : ValueObject<Guid>
{
    /// <inheritdoc />
    private FieldId(Guid value) : base(value)
    {
    }
    
    public static FieldId CreateNew() => new(Guid.NewGuid());
}