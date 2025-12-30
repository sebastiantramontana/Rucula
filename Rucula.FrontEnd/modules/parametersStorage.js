export function saveParameters(p) {
    localStorage.setItem(p.settingsKey, JSON.stringify(p.values));
}

export function getParameters(settingsKey, defaultParameters) {
    const parametersSettingsJson = localStorage.getItem(settingsKey);

    return parametersSettingsJson !== null
        ? JSON.parse(parametersSettingsJson)
        : defaultParameters;
}