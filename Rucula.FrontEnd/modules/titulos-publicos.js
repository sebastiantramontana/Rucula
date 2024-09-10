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

export function showTitulosPublicos(titulos, numberFormater) {
    const table = document.getElementById("tabla-titulos");

    fillTitulosRows(table, titulos, numberFormater);
}

function fillTitulosRows(table, titulos, numberFormater) {
    const tBody = table.tBodies[0];
    const newTbody = tBody.cloneNode(false);
    const row = document.getElementById("titulos-table-row-template").content;

    for (let i = 0; i < titulos.length; i++) {
        addNewRow(newTbody, row, titulos[i], numberFormater);
    }

    tBody.replaceWith(newTbody);
}

function addNewRow(tbody, templateRow, titulo, numberFormater) {
    const clonedRow = templateRow.cloneNode(true);

    writeCell(clonedRow, "symbol-pesos", titulo?.tituloPeso?.simbolo);
    writeCell(clonedRow, "cotizacion-pesos", format(numberFormater, titulo?.tituloPeso?.precioCompra));
    writeCell(clonedRow, "symbol-cable", titulo?.tituloCable?.simbolo);
    writeCell(clonedRow, "cotizacion-cable", format(numberFormater, titulo?.tituloCable?.precioVenta));
    writeCell(clonedRow, "symbol-mep", titulo?.tituloMep?.simbolo);
    writeCell(clonedRow, "cotizacion-mep", format(numberFormater, titulo?.tituloMep?.precioCompra));
    writeCell(clonedRow, "gross-ccl", format(numberFormater, titulo?.grossCcl));
    writeCell(clonedRow, "net-ccl", format(numberFormater, titulo?.netCcl));
    writeCell(clonedRow, "ccl-mep-blue", format(numberFormater, titulo?.cotizacionCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep-blue", format(numberFormater, titulo?.porcentajeArbitrajeCclMepBlue));

    tbody.appendChild(clonedRow);
}

function format(formatter, value) {
    return (value) ? formatter.format(value) : "";
}

function writeCell(row, cellId, data) {
    const cell = row.getElementById(cellId);
    cell.textContent = data ?? "";
}

