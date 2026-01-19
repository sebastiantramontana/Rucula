import { getParameters } from "./parametersStorage.js";

const settingsKey = "parameters-wu-settings";
const defaultParameters = { amountToSend: 2000.0 };

export default function getStoredWUParameters() {
    return getParameters(settingsKey, defaultParameters);
}
