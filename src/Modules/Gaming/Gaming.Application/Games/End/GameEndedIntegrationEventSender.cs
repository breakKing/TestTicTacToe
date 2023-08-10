using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Games.DomainEvents;
using Gaming.IntegrationEvents.Games;

namespace Gaming.Application.Games.End;

internal sealed class GameEndedIntegrationEventSender : IDomainEventHandler<GameEndedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public GameEndedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(GameEndedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new GameEndedIntegrationEvent(
            domainEvent.GameId.Value,
            domainEvent.FinishedAt,
            domainEvent.WinnerPlayerId?.Value,
            domainEvent.LoserPlayerId?.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}