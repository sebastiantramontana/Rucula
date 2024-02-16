namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetStoredElementByIdAsArrayCall : IGetStoredElementByIdAsArrayCall
    {
        public string Generate(string parentObjectName, string id, string elementObjectName)
            => $"globalThis.vitraux.storedElements.getStoredElementByIdAsArray({parentObjectName}, '{parentObjectName}', '{id}', '{elementObjectName}')";
    }
}
