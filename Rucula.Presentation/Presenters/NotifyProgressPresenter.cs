using Rucula.Domain.Abstractions;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class NotifyProgressPresenter(NotifyProgressViewModel viewModel, IViewUpdater<NotifyProgressViewModel> viewUpdater) : INotifier
{
    public Task Notify(string message)
    {
        viewModel.Update(message);
        return viewUpdater.Update(viewModel);
    }
}
