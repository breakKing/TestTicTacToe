﻿using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record GameMoveId : ValueObject<Guid>
{
    /// <inheritdoc />
    private GameMoveId(Guid value) : base(value)
    {
    }

    public static GameMoveId CreateNew() => new(Guid.NewGuid());
}