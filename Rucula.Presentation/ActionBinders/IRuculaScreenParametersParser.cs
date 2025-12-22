namespace Rucula.Presentation.ActionBinders;

internal interface IRuculaScreenParametersParser
{
    RuculaScreenParameters Parse(IDictionary<string, IEnumerable<string>> parameters);
}
