using Rucula.Presentation.Presenters;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaParametersActionBinderAsync(IParametersPresenter presenter, IRuculaParametersParser parser) : ActionParametersBinderAsyncBase<ParametersViewModel>, IRuculaParametersActionBinderAsync
{
    public override Task BindActionAsync(ParametersViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var parsedParams = parser.Parse(parameters);
        return presenter.SaveParameters(viewModel, parsedParams);
    }
}
