using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByTemplate : IStorageElementJsLineGeneratorByTemplate
{
    public string Generate(ElementObjectName elementObjectName)
        => $"globalThis.vitraux.storedElements.getStoredElementByTemplateAsArray('{elementObjectName.AssociatedSelector.Value}', '{elementObjectName.Name}');";
}
