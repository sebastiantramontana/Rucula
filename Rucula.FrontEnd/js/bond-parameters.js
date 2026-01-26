import { getParameters } from "./parametersStorage.js";

const settingsKey = "commisions-bond-settings";

export default function getStoredBondParameters() {
    return getParameters(settingsKey);
}
