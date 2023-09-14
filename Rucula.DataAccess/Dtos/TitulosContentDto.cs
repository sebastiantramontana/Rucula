namespace Rucula.DataAccess.Dtos
{
    public record class TitulosContentDto(PaginationDto PaginationDto, IEnumerable<TituloDto> Titulos);
}
