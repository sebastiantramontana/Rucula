using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers
{
    internal class DolarWesternUnionMapper : IMapper<DolarWesternUnionDto, DolarWesternUnion>
    {
        public DolarWesternUnion Map(DolarWesternUnionDto from)
            => new(from.strikeFxRate);
    }
}
