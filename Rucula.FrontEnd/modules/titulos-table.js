export default function fillTitulosTable(table, titulos) {
    CreateTableHeader(table, titulos);
    FillTitulosRows(table, titulos);
}

function FillTitulosRows(table, titulos) {
    const tbody = table.tBodies[0];
    const row = document.getElementById("titulos-table-row-template").content;

    for (let i = 0; i < titulos.length; i++) {
        AddNewRow(tbody, row, titulos[i]);
    }
}

function AddNewRow(tbody, templateRow, titulo) {
    const clonedRow = templateRow.cloneNode(true);

    WriteCell(clonedRow, "symbol-pesos", titulo.tituloPeso.simbolo);
    WriteCell(clonedRow, "cotizacion-pesos", titulo.tituloPeso.precioVenta);
    WriteCell(clonedRow, "symbol-cable", titulo.tituloCable.simbolo);
    WriteCell(clonedRow, "cotizacion-cable", titulo.tituloCable.precioCompra);
    WriteCell(clonedRow, "symbol-mep", titulo.tituloMep.simbolo);
    WriteCell(clonedRow, "cotizacion-mep", titulo.tituloMep.precioVenta);
    WriteCell(clonedRow, "blue-compra", titulo.blue.precioCompra);
    WriteCell(clonedRow, "blue-venta", titulo.blue.precioVenta);
    WriteCell(clonedRow, "ccl", titulo.cotizacionCcl);
    WriteCell(clonedRow, "ccl-mep-blue", titulo.cotizacionCclMepBlue);
    WriteCell(clonedRow, "porc-ccl-mep-blue", titulo.porcentajeArbitrajeCclMepBlue);
    WriteCell(clonedRow, "porc-ccl-mep", titulo.porcentajeArbitrajeCclMep);
    WriteCell(clonedRow, "porc-mep-blue", titulo.porcentajeRuloMepBlue);

    tbody.appendChild(clonedRow);
}

function WriteCell(row, cellId, data) {
    const cell = row.getElementById(cellId);
    cell.innerHTML = data;
}

function CreateTableHeader(table, titulos) {
    const thead = table.tHead;
    const header = document.getElementById("titulos-table-header-template").content;

    for (let i = 0; i < titulos.length; i++) {
        thead.appendChild(header.cloneNode(true));
    }
}

