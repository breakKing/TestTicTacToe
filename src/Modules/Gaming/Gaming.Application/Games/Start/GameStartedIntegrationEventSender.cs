using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Games.DomainEvents;
using Gaming.IntegrationEvents.Games;

namespace Gaming.Application.Games.Start;

internal sealed class GameStartedIntegrationEventSender : IDomainEventHandler<GameStartedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public GameStartedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(GameStartedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new GameStartedIntegrationEvent(
            domainEvent.GameId.Value,
            domainEvent.FirstPlayerId.Value,
            domainEvent.SecondPlayerId.Value,
            domainEvent.StartedAt);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}