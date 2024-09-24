namespace Rucula.Domain.Entities;

public record class DolarWesternUnion(double GrossPrice, double NetPrice, Optional<double> Fees);
