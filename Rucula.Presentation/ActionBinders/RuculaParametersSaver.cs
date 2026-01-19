using Rucula.Presentation.Presenters;
using Rucula.Presentation.Repositories;

namespace Rucula.Presentation.ActionBinders;

internal sealed class RuculaParametersSaver(IParametersPresenter presenter, IParametersRepository parametersRepository) : IRuculaParametersSaver
{
    public async Task Save()
    {
        var parameters = parametersRepository.GetParameters();

        if (!parameters.IsSuccess)
            throw new InvalidOperationException("Se intentaron guardar parametros inválidos de Rúcula. Ver InnerException", parameters.Exception);

        await presenter.SaveParameters(parameters);

        parametersRepository.Clean();

        await presenter.UpdateUIStateByParameters(parameters, false);
    }
}
