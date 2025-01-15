import ParametersDomHandling from "./ParametersDomHandling.js";

function getInputs() {
    const volumeInput = document.getElementById("trading-volume-crypto");
    return { volumeInput };
}

function getInputsArray() {
    const inputs = getInputs();
    return [inputs.volumeInput];
}

const getApplyButton = () => document.getElementById("apply-trading-volume-crypto");

function getParametersFromInputs() {
    const inputs = getInputs();
    return { volume: parseFloat(inputs.volumeInput.value) };
}

const settingsKey = "parameters-crypto-settings";
const defaultParameters = { volume: 1000.0 };

function setParametersToInputs(parametersSettings) {
    const inputs = getInputs();
    inputs.volumeInput.value = parametersSettings.volume;
}

const cryptoParameters = new ParametersDomHandling(getInputsArray, getApplyButton, getParametersFromInputs, settingsKey, defaultParameters, setParametersToInputs);

export default cryptoParameters;