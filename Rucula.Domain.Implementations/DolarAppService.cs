using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Implementations;

internal sealed class DolarAppService(IDolarAppProvider dolarAppProvider, IDolarNetCalculator dolarNetCalculator) : IDolarAppService
{
    private const double DolarAppFixedFee = 3.0; //DON'T SCRAP THE HELP WEB PAGE!!!

    public async Task<Optional<DolarApp>> GetDolarApp(DolarAppParameters parameters, Action<Optional<DolarApp>> notifyFunc)
    {
        var dolarApp = Optional<DolarApp>.Empty;
        var info = await dolarAppProvider.GetCurrentDolarApp();

        if (info.HasValue)
        {
            var netPrice = CalculateNetPrice(info.Value.GrossPrice, DolarAppFixedFee, parameters);
            dolarApp = Optional<DolarApp>.Sure(new(info.Value.GrossPrice, netPrice, DolarAppFixedFee));
        }

        notifyFunc.Invoke(dolarApp);
        return dolarApp;
    }

    private double CalculateNetPrice(double grossPrice, double fixedFee, DolarAppParameters parameters)
        => dolarNetCalculator.CalculateByFixedFee(grossPrice, parameters.Volume, fixedFee);
}
