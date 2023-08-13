﻿using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Messaging;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.IntegrationEvents.Lobbies;

namespace Gaming.Application.Lobbies.Lock;

internal sealed class LobbyLockedForGameStartIntegrationEventSender : IDomainEventHandler<LobbyLockedForGameStartDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public LobbyLockedForGameStartIntegrationEventSender(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    /// <inheritdoc />
    public async Task Handle(LobbyLockedForGameStartDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new LobbyLockedForGameStartIntegrationEvent(
            domainEvent.LobbyId.Value,
            domainEvent.FirstPlayerId.Value,
            domainEvent.SecondPlayerId.Value);

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}