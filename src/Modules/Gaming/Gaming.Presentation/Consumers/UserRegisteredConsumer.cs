using Gaming.Application.Players.Create;
using Identity.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Gaming.Presentation.Consumers;

public sealed class UserRegisteredConsumer : IConsumer<UserRegisteredIntegrationEvent>
{
    private readonly ISender _sender;

    public UserRegisteredConsumer(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var command = new PlayerCreateCommand(context.Message.UserId, context.Message.Username);
        await _sender.Send(command, context.CancellationToken);
    }
}