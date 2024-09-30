addEventListener("DOMContentLoaded", () => {
    const amountToSendInput = getAmountToSendInput();

    addCurrentDisabledToApplyButton();
    addAmountToSendInputEventListener(amountToSendInput);
    addApplyClickListener();
    loadAmountToSendInput(amountToSendInput);

    function addCurrentDisabledToApplyButton() {
        const applyButton = document.getElementById("apply-amount-to-send-wu");
        applyButton.currentDisabled = applyButton.disabled;
    }

    function addAmountToSendInputEventListener(amountToSendInput) {
        amountToSendInput.addEventListener('input', function () {
            const applyButton = document.getElementById("apply-amount-to-send-wu");
            applyButton.disabled = !amountToSendInput.checkValidity();
            applyButton.currentDisabled = applyButton.disabled;
        });
    }

    function loadAmountToSendInput(amountToSendInput) {
        const parametersWuSettings = getSavedParametersOrDefault();
        amountToSendInput.value = parametersWuSettings.amountToSend;
    }

    function addApplyClickListener() {
        const applyButton = document.getElementById("apply-amount-to-send-wu");
        applyButton.addEventListener("click", saveAmountToSend);
    }

    function saveAmountToSend() {
        const parameters = { "amountToSend": parseFloat(amountToSendInput.value) };
        localStorage.setItem("parameters-wu-settings", JSON.stringify(parameters));
        this.disabled = true;
        this.currentDisabled = true;
    }
});

function getAmountToSendInput() {
    return document.getElementById("amount-to-send-wu");
}

function getSavedParametersOrDefault() {
    const parametersWuSettingsJson = localStorage.getItem("parameters-wu-settings");

    return parametersWuSettingsJson !== null
        ? JSON.parse(parametersWuSettingsJson)
        : { amountToSend: 1000 };
}

function toggleEnablingParameters(isDisabled) {
    const input = getAmountToSendInput();

    input.disabled = isDisabled;

    const applyButton = document.getElementById("apply-amount-to-send-wu");
    applyButton.disabled = (isDisabled) ? true : applyButton.currentDisabled || false;
}

export function disableParameters() {
    toggleEnablingParameters(true);
}

export function enableParameters() {
    toggleEnablingParameters(false);
}

export function getParameters() {
    return getSavedParametersOrDefault();
}
export function showDolarWesternUnion(dolarWesternUnion, numberFormater) {
    showFinalPrice(numberFormater.format(dolarWesternUnion.netPrice));
    showGrossPrice(numberFormater.format(dolarWesternUnion.grossPrice));
    showFees(numberFormater.format(dolarWesternUnion.fees));
    showNetPrice(numberFormater.format(dolarWesternUnion.netPrice));
}

function showFinalPrice(price) {
    showValueInElement("dolar-western-union", price);
}

function showGrossPrice(price) {
    showValueInElement("gross-dolar-western-union", price);
}

function showFees(fees) {
    showValueInElement("gross-fees-western-union", fees);
}

function showNetPrice(price) {
    showValueInElement("net-dolar-western-union", price);
}

function showValueInElement(elementId, value) {
    const element = document.getElementById(elementId);
    element.textContent = value;
}
