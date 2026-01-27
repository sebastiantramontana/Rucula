import { getParameters } from "./parametersStorage.js";

const settingsKey = "parameters-dolarapp-settings";

export default function getStoredDolarAppParameters() {
    return getParameters(settingsKey);
}
