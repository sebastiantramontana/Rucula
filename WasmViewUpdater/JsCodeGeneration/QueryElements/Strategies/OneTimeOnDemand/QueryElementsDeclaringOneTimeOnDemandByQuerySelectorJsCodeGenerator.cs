using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator(IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCalling) : IQueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {getStoredElementsByQuerySelectorCalling.Generate(parentObjectName, selector.Value, elementObjectName)};";
}
