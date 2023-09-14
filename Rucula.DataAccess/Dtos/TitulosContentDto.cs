namespace Rucula.DataAccess.Dtos
{
    public record class TitulosContentDto(PaginationDto paginationDto, IEnumerable<TituloDto> titulos);
}
