namespace Gaming.Application.Common.Messaging;

public interface IMessageBus
{
    ValueTask PublishAsync<TMessage>(TMessage message, CancellationToken ct = default)
        where TMessage : class;
}