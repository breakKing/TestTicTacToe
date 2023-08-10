using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Leave;

internal sealed class LeaveLobbyCommandHandler : ICommandHandler<LeaveLobbyCommand>
{
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyWriteRepository _writeRepository;

    public LeaveLobbyCommandHandler(ILobbyWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(LeaveLobbyCommand request, CancellationToken cancellationToken)
    {
        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _writeRepository.LoadAsync(lobbyId, cancellationToken);

        if (lobby is null)
        {
            return Error.NotFound(description: LobbyNotFoundErrorDescription);
        }
        
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var leaveResult = lobby.PlayerLeave(playerId);

        if (leaveResult.IsError)
        {
            return leaveResult.Errors;
        }
        
        _writeRepository.Update(lobby);

        return true;
    }
}