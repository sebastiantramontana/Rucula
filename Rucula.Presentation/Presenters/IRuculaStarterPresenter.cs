using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.Presenters;

public interface IRuculaStarterPresenter
{
    Task Start(OptionalOptionParameters initialParameters);
}
