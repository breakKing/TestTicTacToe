using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Games.DomainEvents;
using Gaming.IntegrationEvents.Games;

namespace Gaming.Application.Games.Move;

internal sealed class PlayerMovedIntegrationEventSender : IDomainEventHandler<GamePlayerMovedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public PlayerMovedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(GamePlayerMovedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new GamePlayerMovedIntegrationEvent(
            domainEvent.GameId.Value,
            domainEvent.PlayerId.Value,
            domainEvent.Coordinates.Value.X,
            domainEvent.Coordinates.Value.Y,
            domainEvent.MovedAt);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}