namespace Rucula.Domain.Abstractions;

public interface IDolarNetCalculator
{
    double CalculateByFixedFee(double grossPrice, double volume, double fixedFee);
}
