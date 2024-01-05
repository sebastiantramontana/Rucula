using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsDeclaringJsCodeGenerator
{
    string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector elementSelector);
}


