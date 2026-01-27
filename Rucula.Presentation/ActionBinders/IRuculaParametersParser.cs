using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;

namespace Rucula.Presentation.ActionBinders;

internal interface IRuculaParametersParser
{
    Result<OptionParameters> Parse(IDictionary<string, IEnumerable<string>> parameters, ParameterRange bondParameterRange, ParameterRange cryptoParameterRange, ParameterRange wuParameterRange, ParameterRange dolarAppParameterRange);
}
