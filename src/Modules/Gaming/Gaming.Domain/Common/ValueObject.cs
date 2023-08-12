namespace Gaming.Domain.Common;

public abstract record ValueObject<TValue>(TValue Value);
public abstract record ValueObject<TValue1, TValue2>(TValue1 Value1, TValue2 Value2);