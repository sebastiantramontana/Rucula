using Rucula.Domain.Entities.Parameters;
using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.ViewModels.Parameters;

namespace Rucula.Presentation.Presenters;

internal interface IParametersPresenter
{
    Task SaveParameters(ParametersViewModel viewModel, Result<ChoicesParameters> parameters);
}
