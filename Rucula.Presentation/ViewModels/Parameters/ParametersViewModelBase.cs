namespace Rucula.Presentation.ViewModels.Parameters;

internal abstract record class ParametersViewModelBase<TValues>(string SettingsKey) where TValues : new()
{
    internal TValues Values { get; private set; } = new();
    internal void Update(TValues values)
        => Values = values;

    public override abstract string ToString();
    protected string StringifydJson()
        => $$"""
            {
                "key": "{{SettingsKey}}",
                "values": {{GetStringifiedJsonValues()}}
            }
            """;
    protected abstract string GetStringifiedJsonValues();
}

