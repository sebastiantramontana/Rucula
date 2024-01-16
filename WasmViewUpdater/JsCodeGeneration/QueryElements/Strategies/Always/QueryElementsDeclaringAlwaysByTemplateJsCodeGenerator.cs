using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator : IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = [globalThis.vitraux.storedElements.getElementByTemplate('{selector.Value}')];";
}
