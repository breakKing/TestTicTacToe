using Common.Api;
using Gaming.Application.Common.Primitives.Pagination;

namespace Gaming.Presentation.Endpoints;

internal static class EndpointExtensions
{
    private const string NextPageHeader = "X-Next-Page";
    private const string PreviousPageHeader = "X-Next-Page";
    
    public static void PreparePaginationResult<TRequest, TResponse, TData>(
        this EndpointBase<TRequest, TResponse> endpoint, 
        PagedList<TData> pagedList) where TRequest : notnull
    {
        if (pagedList.HasNext)
        {
            var nextPageUrl = $"{endpoint.BaseURL}?pageNumber={pagedList.PageNumber + 1}&pageSize={pagedList.PageSize}";
            endpoint.HttpContext.Response.Headers.Add(NextPageHeader, nextPageUrl);
        }

        if (pagedList.HasPrevious)
        {
            var prevPageUrl = $"{endpoint.BaseURL}?pageNumber={pagedList.PageNumber - 1}&pageSize={pagedList.PageSize}";
            endpoint.HttpContext.Response.Headers.Add(PreviousPageHeader, prevPageUrl);
        }
    }
}