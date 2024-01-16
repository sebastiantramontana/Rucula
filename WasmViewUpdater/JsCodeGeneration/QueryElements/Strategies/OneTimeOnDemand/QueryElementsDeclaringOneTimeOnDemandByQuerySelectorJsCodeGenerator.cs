using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector({parentObjectName}, '{parentObjectName}', '{selector.Value}', '{elementObjectName}');";
}
