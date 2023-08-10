using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Disband;

internal sealed class DisbandLobbyCommandHandler : ICommandHandler<DisbandLobbyCommand>
{
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyWriteRepository _writeRepository;

    public DisbandLobbyCommandHandler(ILobbyWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(DisbandLobbyCommand request, CancellationToken cancellationToken)
    {
        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _writeRepository.LoadAsync(lobbyId, cancellationToken);

        if (lobby is null)
        {
            return Error.NotFound(description: LobbyNotFoundErrorDescription);
        }
        
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var disbandResult = lobby.Disband(playerId);

        if (disbandResult.IsError)
        {
            return disbandResult.Errors;
        }
        
        _writeRepository.Delete(lobby);

        return true;
    }
}