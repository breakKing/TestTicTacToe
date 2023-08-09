using Common.Domain.Primitives;
using MediatR;

namespace Common.Application.Handling;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    
}