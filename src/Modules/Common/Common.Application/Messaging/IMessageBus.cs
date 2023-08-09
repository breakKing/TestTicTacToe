namespace Common.Application.Messaging;

public interface IMessageBus
{
    ValueTask PublishAsync<TMessage>(TMessage message, CancellationToken ct = default);
}