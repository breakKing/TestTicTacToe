using Common.Application.Handling;
using ErrorOr;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Join;

internal sealed class JoinLobbyCommandHandler : ICommandHandler<JoinLobbyCommand>
{
    private const string PlayerInAnotherLobbyErrorDescription = "Игрок уже находится в другом лобби";
    private const string LobbyNotFoundErrorDescription = "Данное лобби не существует";
    
    private readonly ILobbyReadRepository _readRepository;
    private readonly ILobbyWriteRepository _writeRepository;

    public JoinLobbyCommandHandler(ILobbyReadRepository readRepository, ILobbyWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<bool>> Handle(JoinLobbyCommand request, CancellationToken cancellationToken)
    {
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        if (await _readRepository.IsPlayerInLobbyAsync(playerId, cancellationToken))
        {
            return Error.Failure(description: PlayerInAnotherLobbyErrorDescription);
        }

        var lobbyId = LobbyId.CreateFromGuid(request.LobbyId);

        var lobby = await _writeRepository.LoadAsync(lobbyId, cancellationToken);

        if (lobby is null)
        {
            return Error.NotFound(description: LobbyNotFoundErrorDescription);
        }

        var joinResult = lobby.PlayerJoin(playerId);

        if (joinResult.IsError)
        {
            return joinResult.Errors;
        }
        
        _writeRepository.Update(lobby);

        return true;
    }
}