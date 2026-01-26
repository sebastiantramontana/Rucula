import { getParameters } from "./parametersStorage.js";

const settingsKey = "parameters-wu-settings";

export default function getStoredWUParameters() {
    return getParameters(settingsKey);
}
