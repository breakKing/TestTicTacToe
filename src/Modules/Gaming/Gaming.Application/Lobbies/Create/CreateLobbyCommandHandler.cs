using Common.Application.Handling;
using ErrorOr;
using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.Create;

internal sealed class CreateLobbyCommandHandler : ICommandHandler<CreateLobbyCommand, Guid>
{
    private const string PlayerAlreadyInLobbyErrorDescription =
        "Невозможно создать лобби, так как игрок уже находится в лобби";
    
    private readonly ILobbyReadRepository _readRepository;
    private readonly ILobbyWriteRepository _writeRepository;

    public CreateLobbyCommandHandler(ILobbyReadRepository readRepository, ILobbyWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<Guid>> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
    {
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var alreadyInLobby = await _readRepository.IsPlayerInLobbyAsync(playerId, cancellationToken);

        if (alreadyInLobby)
        {
            return Error.Failure(description: PlayerAlreadyInLobbyErrorDescription);
        }

        var lobby = new Lobby(playerId);
        
        _writeRepository.Add(lobby);

        return lobby.Id.Value;
    }
}