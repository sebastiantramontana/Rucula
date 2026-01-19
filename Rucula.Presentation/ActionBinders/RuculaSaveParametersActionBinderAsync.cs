using Rucula.Presentation.Presenters;
using Rucula.Presentation.Repositories;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaValidateParametersActionBinderAsync(IParametersPresenter presenter, IRuculaParametersParser parser, IParametersRepository parametersRepository) : ActionParametersBinderAsyncBase<RuculaScreenViewModel>, IRuculaValidateParametersActionBinderAsync
{
    public override Task BindActionAsync(RuculaScreenViewModel _, IDictionary<string, IEnumerable<string>> parameters)
    {
        const bool ParametersAreDirty = true;

        var parsedParams = parser.Parse(parameters);
        parametersRepository.StoreParameters(parsedParams, ParametersAreDirty);

        return presenter.UpdateUIStateByParameters(parsedParams, ParametersAreDirty);
    }
}
