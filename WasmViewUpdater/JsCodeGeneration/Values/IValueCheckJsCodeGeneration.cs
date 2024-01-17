using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueCheckJsCodeGeneration
{
    string GenerateJsCode(ValueObjectName value, IEnumerable<ElementObjectName> elements);
}
