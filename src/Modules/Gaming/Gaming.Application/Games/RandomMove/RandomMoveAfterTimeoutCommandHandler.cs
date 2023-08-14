using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Persistence;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Application.Games.RandomMove;

internal sealed class RandomMoveAfterTimeoutCommandHandler : ICommandHandler<RandomMoveAfterTimeoutCommand>
{
    private static readonly TimeSpan MoveTime = TimeSpan.FromSeconds(15);

    private readonly IServiceProvider _serviceProvider;

    public RandomMoveAfterTimeoutCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(RandomMoveAfterTimeoutCommand request, CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateAsyncScope();

        Task.Run(() => WaitAndMakeAMove(request, scope, cancellationToken));

        return true;
    }

    private static async Task WaitAndMakeAMove(
        RandomMoveAfterTimeoutCommand request,
        AsyncServiceScope scope,
        CancellationToken cancellationToken)
    {
        await Task.Delay(MoveTime, cancellationToken);

        var writeRepository = scope.ServiceProvider.GetRequiredService<IGameWriteRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var gameId = GameId.CreateFromGuid(request.GameId);
        var game = await writeRepository.LoadAsync(gameId, cancellationToken);

        if (game is null)
        {
            return;
        }

        var moveId = request.LastMoveId.HasValue ? GameMoveId.CreateFromGuid(request.LastMoveId.Value) : null;

        if (game.Moves.Count != 0 &&
            game.Moves.MaxBy(m => m.MovedAt)!.Id != moveId)
        {
            return;
        }

        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        ErrorOr<bool> moveResult;
        do
        {
            var randomX = Random.Shared.Next(0, FieldCoordinates.FieldSize);
            var randomY = Random.Shared.Next(0, FieldCoordinates.FieldSize);

            FieldCoordinates.TryCreate(randomX, randomY, out var fieldCoordinates);

            moveResult = game.Move(playerId, fieldCoordinates);
        } while (moveResult.IsError);

        writeRepository.Update(game);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await scope.DisposeAsync();
    }
}