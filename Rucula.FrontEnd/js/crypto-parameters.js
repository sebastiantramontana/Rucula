import { getParameters } from "./parametersStorage.js";

const settingsKey = "parameters-crypto-settings";

export default function getStoredCryptoParameters() {
    return getParameters(settingsKey);
}
