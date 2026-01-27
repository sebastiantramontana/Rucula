using Rucula.Domain.Abstractions;

namespace Rucula.Domain.Implementations;

internal class DolarNetCalculator : IDolarNetCalculator
{
    public double CalculateByFixedFee(double grossPrice, double volume, double fixedFee)
        => (volume - fixedFee) * grossPrice / volume;
}
