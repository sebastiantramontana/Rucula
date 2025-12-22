using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed class WesternUnionViewModel
{
    public double GrossPrice { get; private set; }
    public double NetPrice { get; private set; }
    public double Fees { get; private set; }

    internal void Update(Optional<DolarWesternUnion> dolarWesternUnion)
    {
        if (dolarWesternUnion.HasValue)
        {
            GrossPrice = dolarWesternUnion.Value.GrossPrice;
            NetPrice = dolarWesternUnion.Value.NetPrice;
            Fees = dolarWesternUnion.Value.Fees;
        }
        else
        {
            GrossPrice = 0.0;
            NetPrice = 0.0;
            Fees = 0.0;
        }
    }
}
