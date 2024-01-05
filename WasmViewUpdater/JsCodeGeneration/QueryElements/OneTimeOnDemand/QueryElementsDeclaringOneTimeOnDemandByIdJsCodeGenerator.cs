using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator : IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = vitraux.getElementById({parentObjectName},'{selector.Value}');";
}
