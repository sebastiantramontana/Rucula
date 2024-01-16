using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByQuerySelector : IStorageElementJsLineGeneratorByQuerySelector
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => $"globalThis.vitraux.storedElements.getStoredElementsByQuerySelector({parentObjectName}, '{parentObjectName}', '{elementObjectName.AssociatedSelector.Value}', '{elementObjectName.Name}');";
}
