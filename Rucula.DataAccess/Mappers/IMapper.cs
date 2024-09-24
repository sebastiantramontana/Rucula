using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Mappers;

internal interface IMapper<TFrom, TTo>
{
    Optional<TTo> Map(Optional<TFrom> from);
}
