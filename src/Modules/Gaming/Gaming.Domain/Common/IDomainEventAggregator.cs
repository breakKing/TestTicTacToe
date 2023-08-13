namespace Gaming.Domain.Common;

public interface IDomainEventAggregator
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}