using Common.Application.Handling;
using ErrorOr;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.GetCurrent;

internal sealed class GetPlayerCurrentLobbyQueryHandler : IQueryHandler<GetPlayerCurrentLobbyQuery, LobbyDto>
{
    private const string PlayerNotInLobbyErrorDescription = "Игрок не состоит ни в одном лобби";
    
    private readonly ILobbyReadRepository _readRepository;

    public GetPlayerCurrentLobbyQueryHandler(ILobbyReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<LobbyDto>> Handle(GetPlayerCurrentLobbyQuery request, CancellationToken cancellationToken)
    {
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var lobby = await _readRepository.GetPlayerLobbyAsync(playerId, cancellationToken);

        if (lobby is null)
        {
            return Error.NotFound(description: PlayerNotInLobbyErrorDescription);
        }

        return lobby;
    }
}