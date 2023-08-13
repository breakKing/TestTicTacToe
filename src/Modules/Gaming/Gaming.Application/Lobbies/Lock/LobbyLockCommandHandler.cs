using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Application.Games;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Lock;

internal sealed class LobbyLockCommandHandler : ICommandHandler<LobbyLockCommand>
{
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyWriteRepository _lobbyWriteRepository;
    private readonly IGameWriteRepository _gameWriteRepository;

    public LobbyLockCommandHandler(ILobbyWriteRepository lobbyWriteRepository, IGameWriteRepository gameWriteRepository)
    {
        _lobbyWriteRepository = lobbyWriteRepository;
        _gameWriteRepository = gameWriteRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(LobbyLockCommand request, CancellationToken cancellationToken)
    {
        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _lobbyWriteRepository.LoadAsync(lobbyId, cancellationToken);
        
        if (lobby is null)
        {
            return Error.NotFound(description: LobbyNotFoundErrorDescription);
        }

        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var startResult = lobby.Lock(playerId);

        if (startResult.IsError)
        {
            return startResult.Errors;
        }
        
        var game = new Game(lobby.InitiatorPlayerId, lobby.JoinedPlayerId!);
        _gameWriteRepository.Add(game);
        
        lobby.SetGame(game);
        _lobbyWriteRepository.Update(lobby);

        return true;
    }
}