namespace Rucula.DataAccess.Dtos
{
    public record class TitulosContentDto(paginationDto paginationDto, IEnumerable<TituloDto> titulos);
}
