using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Dtos;

internal record class DolarWesternUnionDto(double StrikeFxRate, Optional<double> GrossFee);
