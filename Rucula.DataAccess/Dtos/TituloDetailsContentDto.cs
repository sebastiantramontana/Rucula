namespace Rucula.DataAccess.Dtos;

internal sealed record class TituloDetailsContentDto(PaginationDto PaginationDto, IEnumerable<TituloDetailsDto> TitulosDetails);
