namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements
{
    internal class GetElementByIdAsArrayCall : IGetElementByIdAsArrayCall
    {
        public string Generate(string parentObjectName, string id)
            => $"globalThis.vitraux.storedElements.getElementByIdAsArray({parentObjectName},'{id}')";
    }
}
