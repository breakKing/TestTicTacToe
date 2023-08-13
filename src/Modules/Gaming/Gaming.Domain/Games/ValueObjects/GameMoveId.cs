using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameMoveId : ValueObject<Guid>
{
    /// <inheritdoc />
    private GameMoveId(Guid value) : base(value)
    {
    }

    public static GameMoveId CreateNew() => new(Guid.NewGuid());
    public static GameMoveId CreateFromGuid(Guid value) => new(value);
    
    public static implicit operator Guid(GameMoveId gameId) => gameId.Value;
}