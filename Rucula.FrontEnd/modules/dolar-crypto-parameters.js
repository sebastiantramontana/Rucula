addEventListener("DOMContentLoaded", () => {
    const amountToSendInput = getAmountToSendInput();

    addCurrentDisabledToApplyButton();
    addAmountToSendInputEventListener(amountToSendInput);
    addApplyClickListener();
    loadAmountToSendInput(amountToSendInput);

    function addCurrentDisabledToApplyButton() {
        const applyButton = document.getElementById("apply-trading-volume-crypto");
        applyButton.currentDisabled = applyButton.disabled;
    }

    function addAmountToSendInputEventListener(amountToSendInput) {
        amountToSendInput.addEventListener('input', function () {
            const applyButton = document.getElementById("apply-trading-volume-crypto");
            applyButton.disabled = !amountToSendInput.checkValidity();
            applyButton.currentDisabled = applyButton.disabled;
        });
    }

    function loadAmountToSendInput(amountToSendInput) {
        const parametersCryptoSettings = getSavedParametersOrDefault();
        amountToSendInput.value = parametersCryptoSettings.amountToSend;
    }

    function addApplyClickListener() {
        const applyButton = document.getElementById("apply-trading-volume-crypto");
        applyButton.addEventListener("click", saveAmountToSend);
    }

    function saveAmountToSend() {
        const parameters = { "amountToSend": parseFloat(amountToSendInput.value) };
        localStorage.setItem("parameters-crypto-settings", JSON.stringify(parameters));
        this.disabled = true;
        this.currentDisabled = true;
    }
});

function getAmountToSendInput() {
    return document.getElementById("trading-volume-crypto");
}

function getSavedParametersOrDefault() {
    const parametersCryptoSettingsJson = localStorage.getItem("parameters-crypto-settings");

    return parametersCryptoSettingsJson !== null
        ? JSON.parse(parametersCryptoSettingsJson)
        : { amountToSend: 1000 };
}

function toggleEnablingParameters(isDisabled) {
    const input = getAmountToSendInput();

    input.disabled = isDisabled;

    const applyButton = document.getElementById("apply-trading-volume-crypto");
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