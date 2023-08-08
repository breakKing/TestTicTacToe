namespace Common.Application.Primitives.Pagination;

public sealed record PagedList<TData>(
    List<TData> Items,
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => (TotalCount - 1) / PageNumber + 1; // Целочисленное деление с округлением вверх
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public static PagedList<TData> Empty(int pageNumber, int pageSize)
    {
        return new PagedList<TData>(
            new(),
            0,
            pageNumber,
            pageSize);
    }
}