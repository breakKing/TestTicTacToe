using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldId : ValueObject<Guid>
{
    /// <inheritdoc />
    private FieldId(Guid value) : base(value)
    {
    }
    
    public static FieldId CreateNew() => new(Guid.NewGuid());
    public static FieldId CreateFromGuid(Guid value) => new(value);
    
    public static implicit operator Guid(FieldId gameId) => gameId.Value;
}