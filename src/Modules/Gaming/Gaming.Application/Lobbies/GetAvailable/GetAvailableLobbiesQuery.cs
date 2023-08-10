using Gaming.Application.Common.Handling;
using Gaming.Application.Common.Primitives.Pagination;

namespace Gaming.Application.Lobbies.GetAvailable;

public sealed record GetAvailableLobbiesQuery(Guid PlayerId, PaginationRequest PaginationRequest) : 
    IQuery<PagedList<LobbyDto>>;