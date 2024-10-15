addEventListener("DOMContentLoaded", () => {
    const commissionsInputs = getCommissionInputs();

    addCurrentDisabledToApplyButton();
    addAllCommissionInputEventListeners(commissionsInputs);
    addApplyCommissionsClickListener();
    loadCommissionInputs(commissionsInputs);

    function addCurrentDisabledToApplyButton() {
        const applyButton = document.getElementById("apply-bond-commissions-button");
        applyButton.currentDisabled = applyButton.disabled;
    }

    function addAllCommissionInputEventListeners(inputs) {
        addCommissionInputEventListener(inputs.purchaseInput, inputs);
        addCommissionInputEventListener(inputs.saleInput, inputs);
        addCommissionInputEventListener(inputs.withdrawalInput, inputs);
    }

    function addCommissionInputEventListener(inputToAddListener, inputs) {
        inputToAddListener.addEventListener('input', function () {
            const applyButton = document.getElementById("apply-bond-commissions-button");
            applyButton.disabled = !(inputs.purchaseInput.checkValidity() && inputs.saleInput.checkValidity() && inputs.withdrawalInput.checkValidity());
            applyButton.currentDisabled = applyButton.disabled;
        });
    }

    function loadCommissionInputs(inputs) {
        const commisionsBondSettings = getSavedCommisionsOrDefault();

        inputs.purchaseInput.value = commisionsBondSettings.purchasePercentage;
        inputs.saleInput.value = commisionsBondSettings.salePercentage;
        inputs.withdrawalInput.value = commisionsBondSettings.withdrawalPercentage;
    }

    function addApplyCommissionsClickListener() {
        const applyButton = document.getElementById("apply-bond-commissions-button");
        applyButton.addEventListener("click", saveCommissions);
    }

    function saveCommissions() {
        const commissions = { "purchasePercentage": parseFloat(commissionsInputs.purchaseInput.value), "salePercentage": parseFloat(commissionsInputs.saleInput.value), "withdrawalPercentage": parseFloat(commissionsInputs.withdrawalInput.value) };
        localStorage.setItem("commisions-bond-settings", JSON.stringify(commissions));
        this.disabled = true;
        this.currentDisabled = true;
    }
});

function getCommissionInputs() {
    const purchaseInput = document.getElementById("commission-porcentage-purchase-bond");
    const saleInput = document.getElementById("commission-porcentage-sale-bond");
    const withdrawalInput = document.getElementById("commission-porcentage-withdrawal-bond");

    return { purchaseInput, saleInput, withdrawalInput };
}

function getSavedCommisionsOrDefault() {
    const commisionsBondSettingsJson = localStorage.getItem("commisions-bond-settings");

    return commisionsBondSettingsJson !== null
        ? JSON.parse(commisionsBondSettingsJson)
        : { "purchasePercentage": 0.0, "salePercentage": 0.0, "withdrawalPercentage": 0.0 };
}

function toggleEnablingCommissions(isDisabled) {
    const inputs = getCommissionInputs();

    inputs.purchaseInput.disabled = isDisabled;
    inputs.saleInput.disabled = isDisabled;
    inputs.withdrawalInput.disabled = isDisabled;

    const applyButton = document.getElementById("apply-bond-commissions-button");
    applyButton.disabled = (isDisabled) ? true : applyButton.currentDisabled || false;
}

export function disableCommissions() {
    toggleEnablingCommissions(true);
}

export function enableCommissions() {
    toggleEnablingCommissions(false);
}

export function getCommissions() {
    return getSavedCommisionsOrDefault();
}