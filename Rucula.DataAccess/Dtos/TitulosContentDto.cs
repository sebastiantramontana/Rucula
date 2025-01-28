namespace Rucula.DataAccess.Dtos;

internal sealed record class TitulosContentDto(PaginationDto Pagination, IEnumerable<TituloDto> Titulos);
