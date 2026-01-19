using Rucula.Domain.Abstractions;
using Rucula.Presentation.ViewModels;
using Vitraux;

namespace Rucula.Presentation.Presenters;

internal sealed class NotifyProgressPresenter(IViewUpdater<NotifyProgressViewModel> viewUpdater) : INotifier
{
    public Task Notify(string message)
        => viewUpdater.Update(new(message));
}
