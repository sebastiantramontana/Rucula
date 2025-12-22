using Rucula.Domain.Entities;

namespace Rucula.Presentation.ViewModels;

public sealed record class BondViewModel(string ArLabel,
                                         double ArPrice,
                                         string CableLabel,
                                         double CablePrice,
                                         string? MepLabel,
                                         double? MepPrice,
                                         double GrossCcl,
                                         double NetCcl,
                                         double? MepOverCable,
                                         double? BlueOverCcl)
{
    internal static BondViewModel FromEntity(TituloIsin bond) 
        => new(bond.TituloPeso.Simbolo,
               bond.TituloPeso.PrecioCompra,
               bond.TituloCable.Simbolo,
               bond.TituloCable.PrecioVenta,
               bond.TituloMep?.Simbolo ?? string.Empty,
               bond.TituloMep?.PrecioCompra,
               bond.GrossCcl,
               bond.NetCcl,
               bond.MepOverCable,
               bond.BlueOverCcl);
}