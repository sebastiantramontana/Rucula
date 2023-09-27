export default function fillTitulosTable(titulos) {
    const table = document.getElementById("tabla-titulos");

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
    const numFormater = new Intl.NumberFormat('es-AR', {
        minimumFractionDigits: 3,
        maximumFractionDigits: 3,
    });

    WriteCell(clonedRow, "symbol-pesos", titulo.tituloPeso.simbolo);
    WriteCell(clonedRow, "cotizacion-pesos", numFormater.format(titulo.tituloPeso.precioVenta));
    WriteCell(clonedRow, "symbol-cable", titulo.tituloCable.simbolo);
    WriteCell(clonedRow, "cotizacion-cable", numFormater.format(titulo.tituloCable.precioCompra));
    WriteCell(clonedRow, "symbol-mep", titulo.tituloMep.simbolo);
    WriteCell(clonedRow, "cotizacion-mep", numFormater.format(titulo.tituloMep.precioVenta));
    WriteCell(clonedRow, "ccl", numFormater.format(titulo.cotizacionCcl));
    WriteCell(clonedRow, "ccl-mep-blue", numFormater.format(titulo.cotizacionCclMepBlue));
    WriteCell(clonedRow, "porc-ccl-mep-blue", numFormater.format(titulo.porcentajeArbitrajeCclMepBlue));
    WriteCell(clonedRow, "porc-ccl-mep", numFormater.format(titulo.porcentajeArbitrajeCclMep));
    WriteCell(clonedRow, "porc-mep-blue", numFormater.format(titulo.porcentajeRuloMepBlue));

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

