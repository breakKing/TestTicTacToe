using Common.Application.Handling;
using ErrorOr;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Games.GetForPlayer;

internal sealed class GetGameForPlayerQueryHandler : IQueryHandler<GetGameForPlayerQuery, GameDto>
{
    private const string GameNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly IGameReadRepository _readRepository;

    public GetGameForPlayerQueryHandler(IGameReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<GameDto?>> Handle(GetGameForPlayerQuery request, CancellationToken cancellationToken)
    {
        var gameId = GameId.CreateFromGuid(request.GameId);

        var game = await _readRepository.GetByIdAsync(gameId, cancellationToken);

        if (game is null)
        {
            return Error.NotFound(description: GameNotFoundErrorDescription);
        }
        
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        if (playerId.Value != game.FirstPlayer.Id && playerId.Value != game.SecondPlayer.Id)
        {
            return Error.NotFound(description: GameNotFoundErrorDescription);
        }

        return game;
    }
}