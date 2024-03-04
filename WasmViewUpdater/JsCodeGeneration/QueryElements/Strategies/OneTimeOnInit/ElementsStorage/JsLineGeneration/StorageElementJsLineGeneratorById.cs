using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal class StorageElementJsLineGeneratorById(IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCalling) : IStorageElementJsLineGeneratorById
{
    public string Generate(string elementObjectName, string id)
        => $"{getStoredElementByIdAsArrayCalling.Generate(id, elementObjectName)};";
}
