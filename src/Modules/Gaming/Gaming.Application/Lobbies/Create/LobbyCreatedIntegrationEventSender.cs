using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.IntegrationEvents.Lobbies;

namespace Gaming.Application.Lobbies.Create;

internal sealed class LobbyCreatedIntegrationEventSender : IDomainEventHandler<LobbyCreatedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public LobbyCreatedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new LobbyCreatedIntegrationEvent(
            domainEvent.LobbyId.Value,
            domainEvent.InitiatorPlayerId.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}