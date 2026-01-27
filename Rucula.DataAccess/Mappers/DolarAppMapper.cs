using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal sealed class DolarAppMapper : IMapper<DolarAppDto, DolarAppInfo>
{
    public Optional<DolarAppInfo> Map(Optional<DolarAppDto> from)
        => from.HasValue
            ? Optional<DolarAppInfo>.Sure(new(from.Value.GrossPrice))
            : Optional<DolarAppInfo>.Empty;
}
