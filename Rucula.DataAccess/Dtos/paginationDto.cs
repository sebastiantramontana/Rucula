namespace Rucula.DataAccess.Dtos;

public sealed record class PaginationDto(int PageNumber,
                                      int PageCount,
                                      int PageSize,
                                      int TotalElementsCount);
