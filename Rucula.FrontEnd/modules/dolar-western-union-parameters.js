import ParametersDomHandling from "./ParametersDomHandling.js";

function getInputs() {
    const amountToSendInput = document.getElementById("amount-to-send-wu");
    return { amountToSendInput };
}

function getInputsArray() {
    const inputs = getInputs();
    return [inputs.amountToSendInput];
}

const getApplyButton = () => document.getElementById("apply-amount-to-send-wu");

function getParametersFromInputs() {
    const inputs = getInputs();
    return { "amountToSend": parseFloat(inputs.amountToSendInput.value) };
}

const settingsKey = "parameters-wu-settings";
const defaultParameters = { amountToSend: 1000 };

function setParametersToInputs(parametersSettings) {
    const inputs = getInputs();
    inputs.amountToSendInput.value = parametersSettings.amountToSend;
}

const wuParameters = new ParametersDomHandling(getInputsArray, getApplyButton, getParametersFromInputs, settingsKey, defaultParameters, setParametersToInputs);

export default wuParameters;