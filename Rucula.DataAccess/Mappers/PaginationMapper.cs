using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class PaginationMapper : IMapper<PaginationDto, Pagination>
    {
        public Pagination Map(PaginationDto from) => new Pagination(from.PageNumber, from.PageCount, from.PageSize, from.TotalElementsCount);
    }
}
