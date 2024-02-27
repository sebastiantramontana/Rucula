using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator(IGetStoredElementByTemplateAsArrayCall getStoredElementByTemplateAsArrayCall) : IQueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getStoredElementByTemplateAsArrayCall.Generate(elementObjectName.AssociatedSelector.Value, elementObjectName.Name)};";
}
