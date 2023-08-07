using MediatR;

namespace Common.Domain.Primitives;

public abstract record DomainEvent : INotification;