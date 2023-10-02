export default async function showTitulosPublicos() {
    showLoadingIndicator();
    const titulos = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    fillTitulosTable(titulos);
    hideLoadingIndicator();
}

function showLoadingIndicator() {
    let indicator = document.getElementById("titulos-publicos-loading-indicator");
    indicator.style.display = "inline";
}

function hideLoadingIndicator() {
    let indicator = document.getElementById("titulos-publicos-loading-indicator");
    indicator.style.display = "none";
}

function fillTitulosTable(titulos) {
    const table = document.getElementById("tabla-titulos");

    clearTableRows(table);
    createTableHeader(table, titulos);
    fillTitulosRows(table, titulos);
}

function fillTitulosRows(table, titulos) {
    const tbody = table.tBodies[0];
    const row = document.getElementById("titulos-table-row-template").content;

    for (let i = 0; i < titulos.length; i++) {
        addNewRow(tbody, row, titulos[i]);
    }
}

function addNewRow(tbody, templateRow, titulo) {
    const clonedRow = templateRow.cloneNode(true);
    const numFormater = new Intl.NumberFormat('es-AR', {
        minimumFractionDigits: 3,
        maximumFractionDigits: 3,
    });

    writeCell(clonedRow, "symbol-pesos", titulo.tituloPeso.simbolo);
    writeCell(clonedRow, "cotizacion-pesos", numFormater.format(titulo.tituloPeso.precioVenta));
    writeCell(clonedRow, "symbol-cable", titulo.tituloCable.simbolo);
    writeCell(clonedRow, "cotizacion-cable", numFormater.format(titulo.tituloCable.precioCompra));
    writeCell(clonedRow, "symbol-mep", titulo.tituloMep.simbolo);
    writeCell(clonedRow, "cotizacion-mep", numFormater.format(titulo.tituloMep.precioVenta));
    writeCell(clonedRow, "ccl", numFormater.format(titulo.cotizacionCcl));
    writeCell(clonedRow, "ccl-mep-blue", numFormater.format(titulo.cotizacionCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep-blue", numFormater.format(titulo.porcentajeArbitrajeCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep", numFormater.format(titulo.porcentajeArbitrajeCclMep));

    tbody.appendChild(clonedRow);
}

function writeCell(row, cellId, data) {
    const cell = row.getElementById(cellId);
    cell.innerHTML = data;
}

function createTableHeader(table, titulos) {
    const thead = table.tHead;
    const header = document.getElementById("titulos-table-header-template").content;

    for (let i = 0; i < titulos.length; i++) {
        thead.appendChild(header.cloneNode(true));
    }
}

function clearTableRows(table) {
    const rows = table.rows;

    for (let i = 0; i < rows.length; i++)
        table.deleteRow(-1);
}

