namespace Rucula.DataAccess.Dtos;

public record class TitulosContentDto(PaginationDto Pagination, IEnumerable<TituloDto> Titulos);
