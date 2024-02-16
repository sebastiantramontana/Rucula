using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator(IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling) : IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {getStoredElementByIdAsArrayCalling.Generate(parentObjectName, selector.Value, elementObjectName)};";
}
