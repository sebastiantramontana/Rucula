export async function saveParameters(stringifiedJsonSettings) {
    const settings = JSON.parse(stringifiedJsonSettings);
    localStorage.setItem(settings.key, JSON.stringify(settings.values));

    return Promise.resolve();
}

export function getParameters(settingsKey) {
    const parametersSettingsJson = localStorage.getItem(settingsKey);

    return parametersSettingsJson !== null
        ? JSON.parse(parametersSettingsJson)
        : null;
}