using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.IntegrationEvents.Lobbies;

namespace Gaming.Application.Lobbies.Leave;

internal sealed class LobbyLeavedIntegrationEventSender : IDomainEventHandler<LobbyPlayerLeavedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public LobbyLeavedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyPlayerLeavedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new LobbyPlayerLeavedIntegrationEvent(
            domainEvent.LobbyId.Value,
            domainEvent.PlayerId.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}