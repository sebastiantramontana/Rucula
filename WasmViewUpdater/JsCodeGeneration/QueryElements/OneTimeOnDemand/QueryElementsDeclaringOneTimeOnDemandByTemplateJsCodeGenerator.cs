using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = vitraux.getStoredElementByTemplate('{selector.Value}', '{elementObjectName}');";
}
