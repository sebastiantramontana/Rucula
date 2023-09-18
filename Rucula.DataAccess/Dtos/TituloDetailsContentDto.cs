namespace Rucula.DataAccess.Dtos
{
    internal record class TituloDetailsContentDto(PaginationDto PaginationDto, IEnumerable<TituloDetailsDto> TitulosDetails);
}
