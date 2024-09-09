addEventListener("DOMContentLoaded", () => {
    const purchaseInput = document.getElementById("commission-porcentage-purchase-bond");
    const saleInput = document.getElementById("commission-porcentage-sale-bond");
    const withdrawalInput = document.getElementById("commission-porcentage-withdrawal-bond");

    addAllCommissionInputEventListeners();
    addApplyCommissionsClickListener();
    loadInputs();

    function addAllCommissionInputEventListeners() {
        addCommissionInputEventListener(purchaseInput);
        addCommissionInputEventListener(saleInput);
        addCommissionInputEventListener(withdrawalInput);
    }

    function addCommissionInputEventListener(input) {
        input.addEventListener('input', function () {
            const applyButton = document.getElementById("apply-bond-commissions-button");
            applyButton.disabled = !(purchaseInput.checkValidity() && saleInput.checkValidity() && withdrawalInput.checkValidity());
        });
    }

    function loadInputs() {
        const commisionsBondSettings = getSavedCommisionsOrDefault();

        purchaseInput.value = commisionsBondSettings.purchasePercentage;
        saleInput.value = commisionsBondSettings.salePercentage;
        withdrawalInput.value = commisionsBondSettings.withdrawalPercentage;
    }

    function addApplyCommissionsClickListener() {
        const applyButton = document.getElementById("apply-bond-commissions-button");
        applyButton.addEventListener("click", saveCommissions);
    }

    function saveCommissions() {
        const commissions = { "purchasePercentage": parseFloat(purchaseInput.value), "salePercentage": parseFloat(saleInput.value), "withdrawalPercentage": parseFloat(withdrawalInput.value) };
        localStorage.setItem("commisions-bond-settings", JSON.stringify(commissions));
        this.disabled = true;
    }
});

function getSavedCommisionsOrDefault() {
    const commisionsBondSettingsJson = localStorage.getItem("commisions-bond-settings");

    return commisionsBondSettingsJson !== null
        ? JSON.parse(commisionsBondSettingsJson)
        : { "purchasePercentage": 0.0, "salePercentage": 0.0, "withdrawalPercentage": 0.0 };
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

