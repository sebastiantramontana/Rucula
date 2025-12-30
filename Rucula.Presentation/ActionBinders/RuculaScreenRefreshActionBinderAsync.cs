using Rucula.Presentation.Presenters;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaScreenRefreshActionBinderAsync(
    IRuculaScreenPresenter ruculaScreenPresenter,
    IRuculaParametersParser ruculaScreenParametersParser)
    : ActionParametersBinderAsyncBase<RuculaScreenViewModel>, IRuculaScreenRefreshActionBinderAsync
{
    public override Task BindActionAsync(RuculaScreenViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var parsedParams = ruculaScreenParametersParser.Parse(parameters);
        return ruculaScreenPresenter.StartShowChoices(viewModel, parsedParams.Value);
    }
}