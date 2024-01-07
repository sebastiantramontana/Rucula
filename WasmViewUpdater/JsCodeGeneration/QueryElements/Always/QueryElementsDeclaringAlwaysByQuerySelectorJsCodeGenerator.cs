using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator : IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = vitraux.getElementsByQuerySelector({parentObjectName},'{selector.Value}');";
}
