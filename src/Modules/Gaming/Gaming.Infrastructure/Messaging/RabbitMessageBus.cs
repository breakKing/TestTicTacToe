using Gaming.Application.Common.Messaging;
using MassTransit;

namespace Gaming.Infrastructure.Messaging;

internal sealed class RabbitMessageBus : IMessageBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMessageBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    /// <inheritdoc />
    public async ValueTask PublishAsync<TMessage>(TMessage message, CancellationToken ct = default)
        where TMessage : class
    {
        await _publishEndpoint.Publish(message, ct);
    }
}