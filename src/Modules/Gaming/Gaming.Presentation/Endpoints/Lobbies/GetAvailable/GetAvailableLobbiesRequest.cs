using Common.Api;

namespace Gaming.Presentation.Endpoints.Lobbies.GetAvailable;

public sealed record GetAvailableLobbiesRequest(
    int PageNumber, 
    int PageSize) : ApiPaginationRequest(PageNumber, PageSize);