using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsDeclaringOneTimeOnDemandJsCodeGenerator(IJsQueryElementsOneTimeOnDemandGeneratorFactory factory) : IQueryElementsDeclaringOneTimeOnDemandJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => factory
            .GetInstance(elementObjectName.AssociatedSelector)
            .GenerateJsCode(parentObjectName, elementObjectName);
}
