using Gaming.Domain.Common;
using MediatR;

namespace Gaming.Application.Common.Handling;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    
}