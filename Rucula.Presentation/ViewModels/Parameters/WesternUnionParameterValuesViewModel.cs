using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ViewModels.Parameters;

internal sealed class WesternUnionParameterValuesViewModel
{
    internal double AmountToSend { get; private set; } = 1000.0;

    internal void Update(WesternUnionParameters wuParameters)
        => AmountToSend = wuParameters.AmountToSend;
}

