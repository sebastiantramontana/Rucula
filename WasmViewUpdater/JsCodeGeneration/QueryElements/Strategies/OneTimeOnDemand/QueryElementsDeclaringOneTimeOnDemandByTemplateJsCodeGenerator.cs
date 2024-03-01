using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator(
    IGetStoredElementByTemplateAsArrayCall getStoredElementByTemplateAsArrayCall,
    IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory queryGeneratorFactory)
    : IQueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, () => getStoredElementByTemplateAsArrayCall.Generate(elementObjectName.AssociatedSelector.Value, elementObjectName.Name), queryGeneratorFactory);
}