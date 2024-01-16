using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorByQuerySelector : IStorageElementJsLineGeneratorByQuerySelector
{
    public string Generate(ElementObjectName elementObjectName, string parentObjectName)
        => $"vitraux.getStoredElementsByQuerySelector({parentObjectName}, '{parentObjectName}', '{elementObjectName.AssociatedSelector.Value}', '{elementObjectName.Name}');";
}
