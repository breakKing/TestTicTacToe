using Gaming.Application.Common.Primitives.Pagination;

namespace Gaming.Infrastructure.Persistence.Common;

internal static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationRequest pagination)
    {
        return query.Skip(pagination.SkipCount).Take(pagination.PageSize);
    }
}