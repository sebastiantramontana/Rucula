using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator(IGetStoredElementByTemplateAsArrayCall getStoredElementByTemplateAsArrayCall) : IQueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {getStoredElementByTemplateAsArrayCall.Generate(selector.Value, elementObjectName)};";
}
