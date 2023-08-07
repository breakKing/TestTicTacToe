namespace Common.Domain.Primitives;

public abstract record ValueObject<TValue>
{
    public TValue Value { get; init; }

    protected ValueObject(TValue value)
    {
        Value = value;
    }
}