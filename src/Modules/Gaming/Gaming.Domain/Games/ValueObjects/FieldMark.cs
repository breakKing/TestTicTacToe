﻿using Gaming.Domain.Common;

namespace Gaming.Domain.Games.ValueObjects;

public sealed record FieldMark : ValueObject<int>
{
    /// <inheritdoc />
    private FieldMark(int value) : base(value)
    {
    }

    public static FieldMark CreateFromValue(int value) => new(value);

    public static FieldMark NotMarked => new(0);
    public static FieldMark MarkedByFirstPlayer => new(1);
    public static FieldMark MarkedBySecondPlayer => new(2);

    public static implicit operator int(FieldMark fieldMark) => fieldMark.Value;
}