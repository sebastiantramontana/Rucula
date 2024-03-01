using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageFromTemplateElementJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector) : IStorageFromTemplateElementJsLineGenerator
{
    public string Generate(FromTemplateElementSelector selector, string elementObjectName, string parentObjectName)
        => selector.SelectionBy switch
        {
            FromTemplateElementSelection.Id => generatorById.Generate(elementObjectName, selector.Value, parentObjectName),
            FromTemplateElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName, selector.Value, parentObjectName),
            _ => throw new NotImplementedException($"Selector type {selector.SelectionBy} not implemented"),
        };
}