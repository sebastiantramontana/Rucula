using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.JsCodeGeneration;

internal partial class JsCodeExecutor : IJsCodeExecutor
{
#pragma warning disable CA1416 // Validate platform compatibility
    [JSImport("vitraux.ExecuteCode")]
#pragma warning restore CA1416 // Validate platform compatibility
    internal static partial void ExcuteCodeImport(string code);

    public void Excute(string code)
    {
        ExcuteCodeImport(code);
    }
}

