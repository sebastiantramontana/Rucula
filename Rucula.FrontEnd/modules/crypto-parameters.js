import { getParameters } from "./parametersStorage.js";

const settingsKey = "parameters-crypto-settings";
const defaultParameters = { volume: 1000.0 };

export default function getStoredCryptoParameters() {
    return getParameters(settingsKey, defaultParameters);
}
