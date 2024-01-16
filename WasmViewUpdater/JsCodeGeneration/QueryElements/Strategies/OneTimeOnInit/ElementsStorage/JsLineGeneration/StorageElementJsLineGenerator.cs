using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGenerator(
    IStorageElementJsLineGeneratorById generatorById,
    IStorageElementJsLineGeneratorByQuerySelector generatorByQuerySelector,
    IStorageElementJsLineGeneratorByTemplate generatorByTemplate)
    : IStorageElementJsLineGenerator
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => elementObjectName.AssociatedSelector.SelectionBy switch
        {
            ElementSelection.Id => generatorById.Generate(elementObjectName, parentObjectName),
            ElementSelection.QuerySelector => generatorByQuerySelector.Generate(elementObjectName, parentObjectName),
            ElementSelection.Template => generatorByTemplate.Generate(elementObjectName),
            _ => throw new NotImplementedException($"Selector type {elementObjectName.AssociatedSelector.SelectionBy} not implemented for CreateStorageElementJsLine on QueryElementsOneTimeOnInitInitializer"),
        };
}
