using ErrorOr;
using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Primitives.Pagination;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Application.Lobbies.GetAvailable;

internal sealed class GetAvailableLobbiesQueryHandler : IQueryHandler<GetAvailableLobbiesQuery, PagedList<LobbyDto>>
{
    private readonly ILobbyReadRepository _readRepository;

    public GetAvailableLobbiesQueryHandler(ILobbyReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    /// <inheritdoc />
    public async Task<ErrorOr<PagedList<LobbyDto>>> Handle(
        GetAvailableLobbiesQuery request, 
        CancellationToken cancellationToken)
    {
        var playerId = PlayerId.CreateFromGuid(request.PlayerId);

        var alreadyInLobby = await _readRepository.IsPlayerInLobbyAsync(playerId, cancellationToken);

        if (alreadyInLobby)
        {
            return PagedList<LobbyDto>.Empty(
                request.PaginationRequest.PageNumber, 
                request.PaginationRequest.PageSize);
        }

        return await _readRepository.GetAvailableLobbiesAsync(
            playerId,
            request.PaginationRequest,
            cancellationToken);
    }
}