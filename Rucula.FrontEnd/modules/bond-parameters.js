import { getParameters } from "./parametersStorage.js";

const settingsKey = "commisions-bond-settings";
const defaultParameters = { purchasePercentage: 0.0, salePercentage: 0.0, withdrawalPercentage: 0.0 };

export default function getStoredBondParameters() {
    return getParameters(settingsKey, defaultParameters);
}
