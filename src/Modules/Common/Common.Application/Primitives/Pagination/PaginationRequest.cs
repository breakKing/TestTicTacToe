﻿namespace Common.Application.Primitives.Pagination;

public sealed record PaginationRequest(
    int PageNumber,
    int PageSize)
{
    public int SkipCount => (PageNumber - 1) * PageSize;
};