using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.StartGame;

internal sealed class StartGameFromLobbyCommandHandler : ICommandHandler<StartGameFromLobbyCommand>
{
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyWriteRepository _writeRepository;

    public StartGameFromLobbyCommandHandler(ILobbyWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(StartGameFromLobbyCommand request, CancellationToken cancellationToken)
    {
        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _writeRepository.LoadAsync(lobbyId, cancellationToken);
        
        if (lobby is null)
        {
            return Error.NotFound(description: LobbyNotFoundErrorDescription);
        }

        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var startResult = lobby.LockAndStartGame(playerId);

        if (startResult.IsError)
        {
            return startResult.Errors;
        }
        
        _writeRepository.Update(lobby);

        return true;
    }
}