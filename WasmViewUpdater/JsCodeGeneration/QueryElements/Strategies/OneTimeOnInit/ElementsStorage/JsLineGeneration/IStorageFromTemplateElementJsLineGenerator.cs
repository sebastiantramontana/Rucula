using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration
{
    internal interface IStorageFromTemplateElementJsLineGenerator
    {
        string Generate(FromTemplateAppendToElementSelector selector, string elementObjectName, string parentObjectName);
    }
}