using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsCodeGenerator : IQueryElementsDeclaringAlwaysByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = [globalThis.vitraux.storedElements.getElementById({parentObjectName},'{selector.Value}')];";
}
