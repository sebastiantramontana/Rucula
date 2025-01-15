import ParametersDomHandling from "./ParametersDomHandling.js";

function getCommissionInputsArray() {
    const inputs = getCommissionInputs();
    return [inputs.purchaseInput, inputs.saleInput, inputs.withdrawalInput];
}

function getCommissionInputs() {
    const purchaseInput = document.getElementById("commission-porcentage-purchase-bond");
    const saleInput = document.getElementById("commission-porcentage-sale-bond");
    const withdrawalInput = document.getElementById("commission-porcentage-withdrawal-bond");

    return { purchaseInput, saleInput, withdrawalInput };
}

const getApplyButton = () => document.getElementById("apply-bond-commissions-button");

function getParametersFromInputs() {
    const inputs = getCommissionInputs();
    return { purchasePercentage: parseFloat(inputs.purchaseInput.value), salePercentage: parseFloat(inputs.saleInput.value), withdrawalPercentage: parseFloat(inputs.withdrawalInput.value) };
}

const settingsKey = "commisions-bond-settings";
const defaultParameters = { purchasePercentage: 0.0, salePercentage: 0.0, withdrawalPercentage: 0.0 };

function setParametersToInputs(parametersSettings) {
    const inputs = getCommissionInputs();
    inputs.purchaseInput.value = parametersSettings.purchasePercentage;
    inputs.saleInput.value = parametersSettings.salePercentage;
    inputs.withdrawalInput.value = parametersSettings.withdrawalPercentage;
}


const titulosPublicosParameters = new ParametersDomHandling(getCommissionInputsArray, getApplyButton, getParametersFromInputs, settingsKey, defaultParameters, setParametersToInputs);

export default titulosPublicosParameters;