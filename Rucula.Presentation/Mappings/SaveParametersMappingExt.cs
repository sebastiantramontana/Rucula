using Rucula.Presentation.ViewModels.Parameters;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Rucula.Presentation.Mappings;

internal static class SaveParametersMappingExt
{
    private static readonly Uri _saveParametersModuleUri = new("./parametersStorage.js", UriKind.Relative);
    private const string SaveParametersFunctionName = "saveParameters";

    internal static IRootValueFinallizable<SaveParametersViewModel, TValue> ToSaveParametersJs<TValue>(this IRootValueTargetBuilder<SaveParametersViewModel, TValue> builder)
        => builder.ToJsFunction(SaveParametersFunctionName).FromModule(_saveParametersModuleUri);
}
