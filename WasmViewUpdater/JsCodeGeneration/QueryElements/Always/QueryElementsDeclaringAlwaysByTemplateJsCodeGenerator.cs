using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator : IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = [vitraux.getElementByTemplate('{selector.Value}')];";
}
