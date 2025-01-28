namespace Rucula.Domain.Entities;

public sealed record class Pagination(int PageNumber,
                                int PageCount,
                                int PageSize,
                                int TotalElementsCount);
