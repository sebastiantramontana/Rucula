namespace Rucula.Presentation.ViewModels.Parameters;

internal abstract record class ParametersViewModelBase<TValues>(string SettingsKey, TValues Values)
{
    protected string StringifyJson()
        => $$"""
            {
                "key": "{{SettingsKey}}",
                "values": {{GetStringifiedJsonValues()}}
            }
            """;
    protected abstract string GetStringifiedJsonValues();
}

