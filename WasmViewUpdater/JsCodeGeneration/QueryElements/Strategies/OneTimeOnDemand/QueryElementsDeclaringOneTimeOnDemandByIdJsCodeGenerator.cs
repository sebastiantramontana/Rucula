using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator(IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling) : IQueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => $"const {elementObjectName.Name} = {getStoredElementByIdAsArrayCalling.Generate(parentObjectName, elementObjectName.AssociatedSelector.Value, elementObjectName.Name)};";
}
