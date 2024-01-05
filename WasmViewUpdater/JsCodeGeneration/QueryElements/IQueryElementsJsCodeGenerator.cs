using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGenerator
{
    string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName);
}


