using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator : IQueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = globalThis.vitraux.storedElements.getElementsByQuerySelector({parentObjectName},'{selector.Value}');";
}
