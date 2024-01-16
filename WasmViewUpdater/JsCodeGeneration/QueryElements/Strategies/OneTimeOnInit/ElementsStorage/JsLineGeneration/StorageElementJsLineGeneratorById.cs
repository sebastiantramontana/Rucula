using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorById : IStorageElementJsLineGeneratorById
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => $"globalThis.vitraux.storedElements.getStoredElementById({parentObjectName}, '{parentObjectName}', '{elementObjectName.AssociatedSelector.Value}', '{elementObjectName.Name}');";
}
