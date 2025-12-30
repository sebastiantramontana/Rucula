using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class ParametersPresenter(IViewUpdater<ParametersViewModel> viewUpdater) : IParametersPresenter
{
    public Task SaveParameters(ParametersViewModel viewModel, Result<ChoicesParameters> parameters)
    {
        viewModel.Update(parameters.IsSuccess);

        if (parameters.IsSuccess)
            viewModel.Update(parameters.Value);

        return viewUpdater.Update(viewModel);
    }
}