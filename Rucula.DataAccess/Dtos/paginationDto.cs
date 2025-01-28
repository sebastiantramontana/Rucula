namespace Rucula.DataAccess.Dtos;

internal sealed record class PaginationDto(int PageNumber,
                                      int PageCount,
                                      int PageSize,
                                      int TotalElementsCount);
