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
            const applyButton = document.getElementById("apply-commissions-button");
            applyButton.disabled = !(purchaseInput.checkValidity() && saleInput.checkValidity() && withdrawalInput.checkValidity());
        });
    }

    function loadInputs() {
        const commisionsBondSettings = loadSavedCommisionsOrDefault();

        purchaseInput.value = commisionsBondSettings.purchase;
        saleInput.value = commisionsBondSettings.sale;
        withdrawalInput.value = commisionsBondSettings.withdrawal;
    }

    function loadSavedCommisionsOrDefault() {
        const commisionsBondSettingsJson = localStorage.getItem("commisions-bond-settings");

        return commisionsBondSettingsJson !== null
            ? JSON.parse(commisionsBondSettingsJson)
            : { "purchase": 0.0, "sale": 0.0, "withdrawal": 0.0 };
    }

    function addApplyCommissionsClickListener() {
        const applyButton = document.getElementById("apply-commissions-button");
        applyButton.addEventListener("click", applyCommissions);
    }

    function applyCommissions() {
        const commissions = { "purchase": purchaseInput.value, "sale": saleInput.value, "withdrawal": withdrawalInput.value };

        localStorage.setItem("commisions-bond-settings", JSON.stringify(commissions));
    }
});

export default function showTitulosPublicos(titulos, numberFormater) {
    const table = document.getElementById("tabla-titulos");

    clearTableRows(table);
    fillTitulosRows(table, titulos, numberFormater);
}

function fillTitulosRows(table, titulos, numberFormater) {
    const tbody = table.tBodies[0];
    const row = document.getElementById("titulos-table-row-template").content;

    for (let i = 0; i < titulos.length; i++) {
        addNewRow(tbody, row, titulos[i], numberFormater);
    }
}

function addNewRow(tbody, templateRow, titulo, numberFormater) {
    const clonedRow = templateRow.cloneNode(true);

    writeCell(clonedRow, "symbol-pesos", titulo?.tituloPeso?.simbolo);
    writeCell(clonedRow, "cotizacion-pesos", format(numberFormater, titulo?.tituloPeso?.precioCompra));
    writeCell(clonedRow, "symbol-cable", titulo?.tituloCable?.simbolo);
    writeCell(clonedRow, "cotizacion-cable", format(numberFormater, titulo?.tituloCable?.precioVenta));
    writeCell(clonedRow, "symbol-mep", titulo?.tituloMep?.simbolo);
    writeCell(clonedRow, "cotizacion-mep", format(numberFormater, titulo?.tituloMep?.precioCompra));
    writeCell(clonedRow, "ccl", format(numberFormater, titulo?.cotizacionCcl));
    writeCell(clonedRow, "ccl-mep-blue", format(numberFormater, titulo?.cotizacionCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep-blue", format(numberFormater, titulo?.porcentajeArbitrajeCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep", format(numberFormater, titulo?.porcentajeArbitrajeCclMep));

    tbody.appendChild(clonedRow);
}

function format(formatter, value) {
    return (value) ? formatter.format(value) : "";
}

function writeCell(row, cellId, data) {
    const cell = row.getElementById(cellId);
    cell.innerHTML = data ?? "";
}

function clearTableRows(table) {
    const rows = table.tBodies[0].rows;
    const count = rows.length;

    for (let i = 0; i < count; i++)
        table.deleteRow(-1);
}

