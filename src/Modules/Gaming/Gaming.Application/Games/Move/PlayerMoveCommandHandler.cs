using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Games.Move;

internal sealed class PlayerMoveCommandHandler : ICommandHandler<PlayerMoveCommand>
{
    private const string GameNotFoundErrorDescription = "Данная игра не существует";
    private const string InvalidCoordinatesErrorDescription = "Заданы некорректные координаты";
    
    private readonly IGameWriteRepository _writeRepository;

    public PlayerMoveCommandHandler(IGameWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(PlayerMoveCommand request, CancellationToken cancellationToken)
    {
        var gameId = GameId.CreateFromGuid(request.GameId);

        var game = await _writeRepository.LoadAsync(gameId, cancellationToken);

        if (game is null)
        {
            return Error.NotFound(description: GameNotFoundErrorDescription);
        }

        var playerId = PlayerId.CreateFromGuid(request.PlayerId);
        
        if (!FieldCoordinates.TryCreate(request.X, request.Y, out var fieldCoordinates))
        {
            return Error.Validation(description: InvalidCoordinatesErrorDescription);
        }

        var moveResult = game.Move(playerId, fieldCoordinates);

        if (moveResult.IsError)
        {
            return moveResult.Errors;
        }
        
        _writeRepository.Update(game);

        return true;
    }
}