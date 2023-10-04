export default async function showTitulosPublicos(titulos, numberFormater) {
    const table = document.getElementById("tabla-titulos");

    clearTableRows(table);
    createTableHeader(table, titulos);
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

    writeCell(clonedRow, "symbol-pesos", titulo.tituloPeso.simbolo);
    writeCell(clonedRow, "cotizacion-pesos", numberFormater.format(titulo.tituloPeso.precioVenta));
    writeCell(clonedRow, "symbol-cable", titulo.tituloCable.simbolo);
    writeCell(clonedRow, "cotizacion-cable", numberFormater.format(titulo.tituloCable.precioCompra));
    writeCell(clonedRow, "symbol-mep", titulo.tituloMep.simbolo);
    writeCell(clonedRow, "cotizacion-mep", numberFormater.format(titulo.tituloMep.precioVenta));
    writeCell(clonedRow, "ccl", numberFormater.format(titulo.cotizacionCcl));
    writeCell(clonedRow, "ccl-mep-blue", numberFormater.format(titulo.cotizacionCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep-blue", numberFormater.format(titulo.porcentajeArbitrajeCclMepBlue));
    writeCell(clonedRow, "porc-ccl-mep", numberFormater.format(titulo.porcentajeArbitrajeCclMep));

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

