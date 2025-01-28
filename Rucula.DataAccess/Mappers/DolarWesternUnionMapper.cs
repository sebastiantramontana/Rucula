using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal sealed class DolarWesternUnionMapper : IMapper<DolarWesternUnionDto, DolarWesternUnionInfo>
{
    public Optional<DolarWesternUnionInfo> Map(Optional<DolarWesternUnionDto> from)
        => from.HasValue
            ? Optional<DolarWesternUnionInfo>.Sure(new(from.Value.StrikeFxRate, from.Value.GrossFee))
            : Optional<DolarWesternUnionInfo>.Empty;
}
