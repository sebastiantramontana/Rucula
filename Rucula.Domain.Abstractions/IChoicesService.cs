using Rucula.Domain.Entities.Parameters;

namespace Rucula.Domain.Abstractions;

public interface IChoicesService
{
    Task ProcessChoices(ChoicesParameters parameters, ChoicesCallbacks choicesCallbacks);
}
