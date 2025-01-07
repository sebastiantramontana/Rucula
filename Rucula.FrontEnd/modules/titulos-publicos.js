export default function showTitulosPublicos(titulos, numberFormater) {
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
    writeCell(clonedRow, "mep-over-cable", format(numberFormater, titulo?.mepOverCable));
    writeCell(clonedRow, "blue-over-ccl", format(numberFormater, titulo?.blueOverCcl));

    tbody.appendChild(clonedRow);
}

function format(formatter, value) {
    return (value) ? formatter.format(value) : "";
}

function writeCell(row, cellId, data) {
    const cell = row.getElementById(cellId);
    cell.textContent = data ?? "";
}

