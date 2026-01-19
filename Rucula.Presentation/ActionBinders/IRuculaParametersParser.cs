using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ActionBinders;

internal interface IRuculaParametersParser
{
    Result<ChoicesParameters> Parse(IDictionary<string, IEnumerable<string>> parameters);
}
