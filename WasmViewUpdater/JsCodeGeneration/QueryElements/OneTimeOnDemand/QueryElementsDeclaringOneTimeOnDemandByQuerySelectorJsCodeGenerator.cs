using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = vitraux.getElementsByQuerySelector({parentObjectName},'{selector.Value}');";
}
