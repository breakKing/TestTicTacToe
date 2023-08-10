using Gaming.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gaming.Infrastructure.Persistence;

internal sealed class DomainEventPublisherInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DomainEventPublisherInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    /// <inheritdoc />
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var domainEvents = eventData.Context!
            .ChangeTracker
            .Entries<IDomainEventAggregator>()
            .SelectMany(e => e.Entity.DomainEvents);
        
        var finalResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

        await _publisher.Publish(domainEvents, cancellationToken);
        
        return finalResult;
    }
}