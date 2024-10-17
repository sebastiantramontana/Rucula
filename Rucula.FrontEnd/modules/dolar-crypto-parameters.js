import ParametersDomHandling from "./ParametersDomHandling.js";

function getInputs() {
    const amountToSendInput = document.getElementById("trading-volume-crypto");
    return { amountToSendInput };
}

function getInputsArray() {
    const inputs = getInputs();
    return [inputs.amountToSendInput];
}

const getApplyButton = () => document.getElementById("apply-trading-volume-crypto");

function getParametersFromInputs() {
    const inputs = getInputs();
    return { "amountToSend": parseFloat(inputs.amountToSendInput.value) };
}

const settingsKey = "parameters-crypto-settings";
const defaultParameters = { amountToSend: 1000 };

function setParametersToInputs(parametersSettings) {
    const inputs = getInputs();
    inputs.amountToSendInput.value = parametersSettings.amountToSend;
}

const cryptoParameters = new ParametersDomHandling(getInputsArray, getApplyButton, getParametersFromInputs, settingsKey, defaultParameters, setParametersToInputs);

export default cryptoParameters;