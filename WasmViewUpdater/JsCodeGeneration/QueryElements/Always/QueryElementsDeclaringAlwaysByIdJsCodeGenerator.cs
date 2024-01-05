using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class QueryElementsDeclaringAlwaysByIdJsCodeGenerator : IQueryElementsDeclaringAlwaysByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {parentObjectName}.getElementById('{selector.Value}');";
}
