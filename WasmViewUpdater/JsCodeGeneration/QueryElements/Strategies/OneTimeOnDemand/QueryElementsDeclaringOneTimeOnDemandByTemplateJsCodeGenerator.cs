using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = globalThis.vitraux.storedElements.getStoredElementByTemplate('{selector.Value}', '{elementObjectName}');";
}
