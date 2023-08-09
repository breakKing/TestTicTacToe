using Common.Application.Handling;
using Common.Application.Messaging;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.IntegrationEvents.Lobbies;

namespace Gaming.Application.Lobbies.Disband;

internal sealed class LobbyDisbandedIntegrationEventSender : IDomainEventHandler<LobbyDisbandedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public LobbyDisbandedIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyDisbandedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new LobbyDisbandedIntegrationEvent(domainEvent.LobbyId.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}