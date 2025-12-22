using Rucula.Presentation.Presenters;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaScreenRecalculateActionBinderAsync(
    IRuculaScreenPresenter ruculaScreenPresenter,
    IRuculaScreenParametersParser ruculaScreenParametersParser)
    : ActionParametersBinderAsyncBase<RuculaScreenViewModel>, IRuculaScreenRecalculateActionBinderAsync
{
    public override Task BindActionAsync(RuculaScreenViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var parsedParams = ruculaScreenParametersParser.Parse(parameters);
        return ruculaScreenPresenter.ShowRecalculatedChoices(viewModel, parsedParams.BondCommissions, parsedParams.WesternUnionParameters, parsedParams.DolarCryptoParameters);
    }
}
