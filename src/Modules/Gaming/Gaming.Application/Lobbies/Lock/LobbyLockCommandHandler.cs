using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Lock;

internal sealed class LobbyLockCommandHandler : ICommandHandler<LobbyLockCommand>
{
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyWriteRepository _writeRepository;

    public LobbyLockCommandHandler(ILobbyWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(LobbyLockCommand request, CancellationToken cancellationToken)
    {
        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _writeRepository.LoadAsync(lobbyId, cancellationToken);
        
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
        
        _writeRepository.Update(lobby);

        return true;
    }
}