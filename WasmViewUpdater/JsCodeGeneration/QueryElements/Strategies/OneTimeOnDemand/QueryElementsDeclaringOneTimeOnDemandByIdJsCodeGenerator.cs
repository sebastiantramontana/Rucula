using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = globalThis.vitraux.storedElements.getStoredElementById({parentObjectName}, '{parentObjectName}', '{selector.Value}', '{elementObjectName}');";
}
