using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsDeclaringOneTimeOnInitJsCodeGenerator : IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{elementObjectName};";
}
