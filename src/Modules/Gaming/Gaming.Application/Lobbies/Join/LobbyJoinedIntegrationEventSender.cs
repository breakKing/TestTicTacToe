using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.IntegrationEvents.Lobbies;

namespace Gaming.Application.Lobbies.Join;

internal sealed class LobbyJoinedIntegrationEventSender : IDomainEventHandler<LobbyPlayerJoinedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public LobbyJoinedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyPlayerJoinedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new LobbyPlayerJoinedIntegrationEvent(
            domainEvent.LobbyId.Value,
            domainEvent.PlayerId.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}