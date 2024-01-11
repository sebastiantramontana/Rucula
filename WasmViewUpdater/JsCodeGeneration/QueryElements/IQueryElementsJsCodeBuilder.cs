using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeBuilder
{
    string BuildJsCode(IQueryElementsDeclaringJsCodeGenerator declaringJsCodeGenerator, IEnumerable<ElementSelector> selectors, string parentObjectName);
}


