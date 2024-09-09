using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.Domain.Implementations;

internal class TitulosService : ITitulosService
{
    private readonly ITitulosProvider _titulosProvider;
    private readonly ITituloDetailsProvider _tituloDetailsProvider;
    private readonly INotifier _notifier;

    public TitulosService(ITitulosProvider titulosProvider, ITituloDetailsProvider tituloDetailsProvider, INotifier notifier)
    {
        _titulosProvider = titulosProvider;
        _tituloDetailsProvider = tituloDetailsProvider;
        _notifier = notifier;
    }

    public IEnumerable<TituloIsin> RecalculateNetCclRanking(IEnumerable<TituloIsin> titulos, BondCommissions bondCommissions)
        => titulos
            .Select(t => t with { NetCcl = CalculateNetCcl(t.GrossCcl, bondCommissions) })
            .OrderByDescending(t => t.NetCcl);

    public async Task<IEnumerable<TituloIsin>> GetNetCclRanking(Blue blue, BondCommissions bondCommissions)
    {
        await _notifier.NotifyProgress("Consultando títulos públicos...").ConfigureAwait(false);
        var titulos = await _titulosProvider.Get().ConfigureAwait(false);
        var details = await GetUsefulTitulosDetails(titulos).ConfigureAwait(false);

        return CreateTitulosIsin(details, blue, bondCommissions);
    }

    private async Task<IEnumerable<(Titulo Titulo, TituloDetails? Details)>> GetUsefulTitulosDetails(IEnumerable<Titulo> titulos)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var detailsContentList = new List<(Titulo Titulo, TituloDetails? Details)>();

        foreach (var t in titulos)
        {
            await _notifier.NotifyProgress($"Consultando {t.Simbolo}...").ConfigureAwait(false);
            var allDetails = await GetTituloDetails(t).ConfigureAwait(false);
            var usefulDetails = allDetails.FirstOrDefault(d => IsDetailsUseful(d, today));
            detailsContentList.Add((t, usefulDetails));
        }

        return detailsContentList;
    }

    private Task<IEnumerable<TituloDetails>> GetTituloDetails(Titulo titulo)
        => _tituloDetailsProvider.GetTituloDetails(titulo.Simbolo);

    private static IEnumerable<TituloIsin> CreateTitulosIsin(IEnumerable<(Titulo Titulo, TituloDetails? TituloDetails)> details, Blue blue, BondCommissions bondCommissions)
        => details
            .Where(d => d.TituloDetails is not null)
            .GroupBy(d => d.TituloDetails!)
            .Select(g => CreateTituloIsin(g, blue, bondCommissions))
            .Where(t => t.NetCcl is not null)
            .OrderByDescending(t => t.NetCcl);

    private static TituloIsin CreateTituloIsin(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails? TituloDetails)> values, Blue blue, BondCommissions bondCommissions)
    {
        var isin = values.Key.CodigoIsin;
        var denominacion = values.Key.Denominacion;
        var tituloCable = GetTitulo(values, Moneda.DolarCable);
        var tituloPeso = GetTitulo(values, Moneda.Peso);
        var tituloMep = GetTitulo(values, Moneda.DolarMep);
        var fechaVencimiento = values.Key.FechaVencimiento;
        var grossCcl = CalculateGrossCcl(tituloPeso, tituloCable);
        var netCcl = CalculateNetCcl(grossCcl, bondCommissions);
        var mep = CalculateMep(tituloPeso, tituloMep);
        var cclMepBlue = CalculateCclMepBlue(blue, netCcl, mep);
        var percCclMepBlue = CalculatePercentageCclMepBlue(cclMepBlue, netCcl);

        return new(isin,
                   denominacion,
                   tituloCable,
                   tituloPeso,
                   tituloMep,
                   fechaVencimiento,
                   grossCcl,
                   netCcl,
                   cclMepBlue,
                   percCclMepBlue);
    }

    private static Titulo? GetTitulo(IGrouping<TituloDetails, (Titulo Titulo, TituloDetails? TituloDetails)> tuples, Moneda moneda)
        => tuples
            .Select(t => t.Titulo)
            .SingleOrDefault(t => IsTituloUseful(t, moneda));

    private static bool IsDetailsUseful(TituloDetails tituloDetails, DateOnly today)
        => tituloDetails.FechaVencimiento > today
            && tituloDetails.TipoObligacion == TipoObligacion.Nacional;

    private static bool IsTituloUseful(Titulo titulo, Moneda moneda)
        => (titulo.PrecioCompra > 0.0 || titulo.PrecioVenta > 0.0)
            && titulo.Moneda == moneda
            && titulo.Parking == Parking.CI;

    private static double? CalculateGrossCcl(Titulo? tituloPeso, Titulo? tituloCable)
        => Divide(tituloPeso?.PrecioCompra, tituloCable?.PrecioVenta);

    private static double? CalculateNetCcl(double? grossCcl, BondCommissions commissions)
    {
        var commisionsList = new[]
        {
            commissions.PurchasePercentage,
            commissions.SalePercentage,
            commissions.WithdrawalPercentage
        };

        return commisionsList.Aggregate(grossCcl, SubstractPercentage);
    }

    private static double? SubstractPercentage(double? value, double percentage)
        => value.HasValue ? value.Value * (1.0 - percentage / 100) : null;

    private static double? CalculateCclMepBlue(Blue blue, double? ccl, double? mep)
        => blue.PrecioCompra * Divide(ccl, mep);

    private static double? CalculatePercentageCclMepBlue(double? cclMepBlue, double? ccl)
        => ConvertToPercentaje(Divide(cclMepBlue, ccl));

    private static double? CalculateMep(Titulo? tituloPesos, Titulo? tituloMep)
        => Divide(tituloPesos?.PrecioVenta, tituloMep?.PrecioCompra);

    private static double? ConvertToPercentaje(double? valor)
        => 100 * valor - 100;

    private static double? Divide(double? value1, double? value2)
        => value1.HasValue && value2.HasValue && value2.Value != 0
            ? value1.Value / value2.Value
            : null;
}
