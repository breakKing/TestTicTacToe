using Gaming.Application.Common.Handling;
using Gaming.Application.Games.RandomMove;
using Gaming.Domain.Games.DomainEvents;
using MediatR;

namespace Gaming.Application.Games.Move;

internal sealed class PlayerMoveTimerHandler :
    IDomainEventHandler<GameStartedDomainEvent>,
    IDomainEventHandler<GamePlayerMovedDomainEvent>
{
    private readonly ISender _sender;
    private readonly IGameWriteRepository _writeRepository;
    
    public PlayerMoveTimerHandler(ISender sender, IGameWriteRepository writeRepository)
    {
        _sender = sender;
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public Task Handle(GameStartedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var command = new RandomMoveAfterTimeoutCommand(
            domainEvent.GameId,
            domainEvent.FirstPlayerId,
            null);
        
        return _sender.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public Task Handle(GamePlayerMovedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var game = _writeRepository.LoadAsync(domainEvent.GameId, cancellationToken)
            .GetAwaiter()
            .GetResult();

        if (game is null)
        {
            return Task.CompletedTask;
        }
        
        var command = new RandomMoveAfterTimeoutCommand(
            domainEvent.GameId,
            game.LastMovePlayerId == game.FirstPlayerId ? game.SecondPlayerId : game.FirstPlayerId,
            domainEvent.MoveId);
        
        return _sender.Send(command, cancellationToken);
    }
}