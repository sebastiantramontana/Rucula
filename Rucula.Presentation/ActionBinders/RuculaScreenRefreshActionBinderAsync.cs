using Rucula.Presentation.Presenters;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaScreenRefreshActionBinderAsync(
    IRuculaScreenPresenter ruculaScreenPresenter,
    IRuculaScreenParametersParser ruculaScreenParametersParser)
    : ActionParametersBinderAsyncBase<RuculaScreenViewModel>, IRuculaScreenRefreshActionBinderAsync
{
    public override Task BindActionAsync(RuculaScreenViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var parsedParams = ruculaScreenParametersParser.Parse(parameters);
        return ruculaScreenPresenter.ShowChoices(viewModel, parsedParams.BondCommissions, parsedParams.WesternUnionParameters, parsedParams.DolarCryptoParameters);
    }
}