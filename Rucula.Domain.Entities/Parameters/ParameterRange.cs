namespace Rucula.Domain.Entities.Parameters;

public readonly record struct ParameterRange
{
    public ParameterRange(double min, double max)
    {
        Min = min;
        Max = max;
    }

    public double Min { get; }
    public double Max { get; }

    public bool Contains(double value)
        => (!double.IsNaN(value)) && value >= Min && value <= Max;

    public override string ToString()
        => $"{Min} - {Max}";
}

