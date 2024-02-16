namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating
{
    internal interface IUpdateByTemplateCall
    {
        string Generate(string templateContentArg, string appendToElementsArg, string toChildQueryFunctionArg, string updateTemplateChildFunctionArg);
    }
}