namespace Rucula.Domain.Entities;

public sealed record class DolarWesternUnion(double GrossPrice, double NetPrice, double FixedFee);
