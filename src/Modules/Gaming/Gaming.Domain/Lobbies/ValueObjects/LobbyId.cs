using Common.Domain.Primitives;

namespace Gaming.Domain.Lobbies.ValueObjects;

public sealed record LobbyId : ValueObject<Guid>
{
    /// <inheritdoc />
    private LobbyId(Guid value) : base(value)
    {
    }

    public static LobbyId CreateNew() => new(Guid.NewGuid());
}