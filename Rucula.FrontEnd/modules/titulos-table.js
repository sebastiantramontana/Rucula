export default function fillTitulosTable(table, titulos) {
    CreateTableHeader(table, titulos);
    FillTitulosRows(table, titulos);
}

function FillTitulosRows(table, titulos) {
    const tbody = table.tBodies[0];
    const row = document.getElementById("titulos-table-row-template").content;

    for (let i = 0; i < titulos.length; i++) {
        const clonedRow = row.cloneNode(true);
        const cell = clonedRow.getElementById("symbol-pesos");
        cell.innerHTML = "lalala";
        tbody.appendChild(clonedRow);
    }
}

function CreateTableHeader(table, titulos) {
    const thead = table.tHead;
    const header = document.getElementById("titulos-table-header-template").content;

    for (let i = 0; i < titulos.length; i++) {
        thead.appendChild(header.cloneNode(true));
    }
}

