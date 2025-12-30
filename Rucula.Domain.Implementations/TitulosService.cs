using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class TitulosService(ITitulosProvider titulosProvider, ITituloDetailsProvider tituloDetailsProvider, INotifier notifier) : ITitulosService
{
    public async Task<IEnumerable<TituloIsin>> GetNetCclRanking(Optional<Blue> blue, BondCommissions bondCommissions, Func<IEnumerable<TituloIsin>, Task> notifyFunc)
    {
        await notifier.Notify("Consultando títulos públicos...");
        var titulos = await titulosProvider.Get();
        var details = await GetUsefulTitulosDetails(titulos);
        var bonds = CreateTitulosIsin(details, blue, bondCommissions);

        await notifyFunc.Invoke(bonds);

        return bonds;
    }

    private async Task<IEnumerable<(Titulo Titulo, TituloDetails Details)>> GetUsefulTitulosDetails(IEnumerable<Titulo> titulos)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var detailsContentList = new List<(Titulo Titulo, TituloDetails Details)>();

        foreach (var titulo in titulos)
        {
            if (IsTituloValid(titulo))
            {
                await notifier.Notify($"Consultando {titulo.Simbolo}...");
                var allDetails = await GetTituloDetails(titulo);
                var usefulDetails = allDetails.FirstOrDefault(d => IsDetailsUseful(d, today));

                if (usefulDetails is not null)
                    detailsContentList.Add((titulo, usefulDetails));
            }
        }

        return detailsContentList;
    }

    private Task<IEnumerable<TituloDetails>> GetTituloDetails(Titulo titulo)
        => tituloDetailsProvider.GetTituloDetails(titulo.Simbolo);

    private static IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(Titulo Titulo, TituloDetails TituloDetails)> details, Optional<Blue> blue, BondCommissions bondCommissions)
        => details
            .Where(d => d.TituloDetails is not null)
            .GroupBy(d => d.TituloDetails!)
            .Where(FilterGroupedBondsByRequiredCurrencies)
            .Select(g => CreateTituloIsin(g, blue, bondCommissions))
            .OrderByDescending(t => t.NetCcl);

    private static bool FilterGroupedBondsByRequiredCurrencies(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> groupedBonds)
        => groupedBonds.Any(t => t.Titulo.Moneda == Moneda.Peso) && groupedBonds.Any(t => t.Titulo.Moneda == Moneda.DolarCable);

    private static TituloIsin CreateTituloIsin(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> values, Optional<Blue> blue, BondCommissions bondCommissions)
    {
        var isin = values.Key.CodigoIsin;
        var denominacion = values.Key.Denominacion;
        var tituloCable = GetRequiredTitulo(values, Moneda.DolarCable);
        var tituloPeso = GetRequiredTitulo(values, Moneda.Peso);
        var tituloMep = GetOptionalTitulo(values, Moneda.DolarMep);
        var fechaVencimiento = values.Key.FechaVencimiento;
        var grossCcl = CalculateGrossCcl(tituloPeso, tituloCable);
        var netCcl = CalculateNetCcl(grossCcl, bondCommissions);
        var mepOverCable = CalculateMepOverCable(tituloMep, tituloCable);
        var blueOverCcl = CalculateBlueOverCcl(blue, tituloMep, tituloPeso);

        return new(isin,
                   denominacion,
                   tituloCable,
                   tituloPeso,
                   tituloMep,
                   fechaVencimiento,
                   grossCcl,
                   netCcl,
                   mepOverCable,
                   blueOverCcl);
    }

    private static Titulo GetRequiredTitulo(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> tuples, Moneda moneda)
         => tuples
            .Select(t => t.Titulo)
            .Where(t => HasTituloCurrency(t, moneda))
            .Single();

    private static Titulo? GetOptionalTitulo(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails TituloDetails)> tuples, Moneda moneda)
        => tuples
            .Select(t => t.Titulo)
            .SingleOrDefault(t => HasTituloCurrency(t, moneda));

    private static bool IsDetailsUseful(TituloDetails tituloDetails, DateOnly today)
        => tituloDetails.FechaVencimiento > today
            && tituloDetails.TipoObligacion == TipoObligacion.Nacional;

    private static bool IsTituloValid(Titulo titulo)
        => (titulo.PrecioCompra > 0.0 || titulo.PrecioVenta > 0.0)
            && titulo.Parking == Parking.CI;

    private static bool HasTituloCurrency(Titulo titulo, Moneda moneda)
        => titulo.Moneda == moneda;

    private static double CalculateGrossCcl(Titulo tituloPeso, Titulo tituloCable)
        => Divide(tituloPeso.PrecioCompra, tituloCable.PrecioVenta);

    private static double CalculateNetCcl(double grossCcl, BondCommissions commissions)
    {
        var commisionsList = new[]
        {
            commissions.PurchasePercentage,
            commissions.SalePercentage,
            commissions.WithdrawalPercentage
        };

        return commisionsList.Aggregate(grossCcl, SubstractPercentage);
    }

    private static double SubstractPercentage(double value, double percentage)
        => value * (1.0 - percentage / 100);

    private static double? CalculateMepOverCable(Titulo? tituloMep, Titulo? tituloCable)
        => Divide(tituloMep?.PrecioCompra, tituloCable?.PrecioVenta);

    private static double? CalculateBlueOverCcl(Optional<Blue> blue, Titulo? tituloMep, Titulo? tituloPesos)
        => blue.HasValue ? Divide(Multiply(blue.Value.PrecioCompra, tituloMep?.PrecioCompra), tituloPesos?.PrecioCompra) : null;

    private static double? Divide(double? value1, double? value2)
        => value1.HasValue && value2.HasValue && value2.Value != 0
            ? value1.Value / value2.Value
            : null;

    private static double Divide(double value1, double value2)
        => value2 != 0 ? value1 / value2 : 0;

    private static double? Multiply(double? value1, double? value2)
        => value1.HasValue && value2.HasValue
            ? value1.Value * value2.Value
            : null;
}
