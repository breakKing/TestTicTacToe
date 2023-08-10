using MediatR;

namespace Gaming.Domain.Common;

public abstract record DomainEvent : INotification;