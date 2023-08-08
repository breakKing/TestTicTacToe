using Common.Application.Handling;
using Common.Application.Primitives.Pagination;

namespace Gaming.Application.Lobbies.GetAvailable;

public sealed record GetAvailableLobbiesQuery(Guid PlayerId, PaginationRequest PaginationRequest) : 
    IQuery<PagedList<LobbyDto>>;