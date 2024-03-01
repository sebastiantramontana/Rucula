using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
    {
        string GenerateAppendToJsCode(string appendToObjectName, FromTemplateElementSelector elementToAppend, IJsQueryFromTemplateElementsDeclaringGeneratorFactory jsGeneratorFactory);
    }
}