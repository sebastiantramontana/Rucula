namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating
{
    internal class UpdateByTemplateCall : IUpdateByTemplateCall
    {
        public string Generate(string templateContentArg, string appendToElementsArg, string toChildQueryFunctionArg, string updateTemplateChildFunctionArg)
            => $"globalThis.vitraux.updating.UpdateByTemplate({templateContentArg}, {appendToElementsArg}, {toChildQueryFunctionArg}, {updateTemplateChildFunctionArg})";
    }
}
