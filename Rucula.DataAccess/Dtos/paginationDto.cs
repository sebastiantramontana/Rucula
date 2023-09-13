namespace Rucula.DataAccess.Dtos
{
    public sealed record class paginationDto(int PageNumber,
                                             int PageCount,
                                             int PageSize,
                                             int TotalElementsCount);
}
