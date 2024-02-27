using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsDeclaringJsCodeGenerator
{
    string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName);
}


