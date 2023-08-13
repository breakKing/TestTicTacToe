using System.Transactions;
using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Domain.Games.DomainEvents;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Application.Games.Move;

internal sealed class PlayerMoveTimerHandler :
    IDomainEventHandler<GameStartedDomainEvent>,
    IDomainEventHandler<GamePlayerMovedDomainEvent>
{
    private static readonly TimeSpan MoveTime = TimeSpan.FromSeconds(120);

    private readonly IServiceProvider _serviceProvider;

    public PlayerMoveTimerHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public Task Handle(GameStartedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Task.Run(() => CountdownAndRandomMoveAsync(
            domainEvent.GameId,
            domainEvent.FirstPlayerId,
            null,
            cancellationToken));
        
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Handle(GamePlayerMovedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Task.Run(() => CountdownAndRandomMoveAsync(
            domainEvent.GameId,
            domainEvent.PlayerId,
            domainEvent.MoveId,
            cancellationToken));
        
        return Task.CompletedTask;
    }

    private async Task CountdownAndRandomMoveAsync(
        GameId gameId, 
        PlayerId playerId, 
        GameMoveId? lastMoveId, 
        CancellationToken ct = default)
    {
        await Task.Delay(MoveTime, ct);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var writeRepository = scope.ServiceProvider.GetRequiredService<IGameWriteRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var game = await writeRepository.LoadAsync(gameId, ct);

        if (game is null)
        {
            return;
        }

        if (game.Moves.Count != 0 &&
            game.Moves.MaxBy(m => m.MovedAt)!.Id != lastMoveId)
        {
            return;
        }

        ErrorOr<bool> moveResult;
        do
        {
            var randomX = Random.Shared.Next(0, FieldCoordinates.FieldSize);
            var randomY = Random.Shared.Next(0, FieldCoordinates.FieldSize);
            
            FieldCoordinates.TryCreate(randomX, randomY, out var fieldCoordinates);

            moveResult = game.Move(playerId, fieldCoordinates);
        } while (moveResult.IsError);
        
        writeRepository.Update(game);

        await unitOfWork.SaveChangesAsync(ct);
        
        transactionScope.Complete();
    }
}