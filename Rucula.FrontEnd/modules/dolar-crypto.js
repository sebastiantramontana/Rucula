export default function showDolarCrypto(cryptoPrices, numberFormater) {
    const table = document.getElementById("tabla-crypto");
    const newTable = table.cloneNode(true);

    removePreviousTBodies(newTable);
    const tbodies = createExchangeBodies(cryptoPrices, numberFormater);
    tbodies.forEach(tbody => newTable.appendChild(tbody));

    table.replaceWith(newTable);
}

function removePreviousTBodies(table) {
    const count = table.tBodies.length;

    for (let i = 0; i < count; i++) {
        table.removeChild(table.tBodies[0]);
    };
}

function createExchangeBodies(cryptoPrices, numberFormater) {

    const tbodies = [];
    const tbodyTemplate = document.getElementById("crypto-exchange-tbody-template").content.querySelector("tbody");
    let arePreviousRowsOdd = false;

    cryptoPrices.forEach(cryptoPrice => {
        const tbody = tbodyTemplate.cloneNode(true);

        setRowSpanToExchangeCells(tbody, cryptoPrice.netPrices, arePreviousRowsOdd);
        writeExchangeCells(tbody, cryptoPrice, numberFormater);
        addBlockchainRowsToTBody(tbody, cryptoPrice.netPrices, numberFormater);

        tbodies.push(tbody);
        arePreviousRowsOdd = isOdd(cryptoPrice.netPrices.length);
    });

    return tbodies;
}

function addBlockchainRowsToTBody(tbody, netPrices, numberFormater) {
    const rows = createBlockchainNetRows(netPrices, numberFormater);
    rows.forEach(row => tbody.appendChild(row));
}

function setRowSpanToExchangeCells(tbody, netPrices, arePreviousRowsOdd) {
    if (arePreviousRowsOdd)
        addHiddenBalancingAlternatingColorFirstRow(tbody);

    const rowspan = calculateRowSpanForExchangeCell(netPrices, arePreviousRowsOdd);
    setRowSpanAttributeToExchangeCells(tbody, rowspan);
}

function writeExchangeCells(tbody, cryptoPrice, numberFormater) {
    const firstRow = tbody.firstElementChild;

    writeCell(firstRow, "exchange", cryptoPrice.exchange);
    writeCell(firstRow, "gross-usdc", format(numberFormater, cryptoPrice.grossUsdc));
    writeCell(firstRow, "gross-usdt", format(numberFormater, cryptoPrice.grossUsdt));
    writeCell(firstRow, "gross-dai", format(numberFormater, cryptoPrice.grossDai));

}

function isOdd(number) {
    return number % 2 !== 0;
}

function calculateRowSpanForExchangeCell(netPrices, arePreviousRowsOdd) {
    var rowspan = netPrices.length + 1;
    return incrementRowspanForHiddenBalancingAlternatingColorFirstRow(rowspan, arePreviousRowsOdd);
}

function incrementRowspanForHiddenBalancingAlternatingColorFirstRow(rowspan, arePreviousRowsOdd) {
    if (arePreviousRowsOdd)
        rowspan++;

    return rowspan;
}

function addHiddenBalancingAlternatingColorFirstRow(tbody) {
    tbody.appendChild(document.createElement("tr"));
}

function setRowSpanAttributeToExchangeCells(tbody, rowspan) {
    const exchangeCells = getRowSpannedCells(tbody);
    exchangeCells.forEach(cell => cell.setAttribute("rowspan", rowspan));
}

function getRowSpannedCells(tbody) {
    return tbody.querySelectorAll("td[rowspanned]");
}

function createBlockchainNetRows(netPrices, numberFormater) {
    const rows = [];
    const rowTemplate = document.getElementById("crypto-blockchain-net-row-template").content.querySelector("tr");

    netPrices.forEach(netPrice => {
        const row = rowTemplate.cloneNode(true);
        writeBlockchainCells(row, netPrice, numberFormater);

        rows.push(row);
    });

    return rows;
}

function writeBlockchainCells(row, netPrice, numberFormater) {
    writeCell(row, "blockchain-name", netPrice.blockchain.name);
    writeCell(row, "blockchain-commission", format(numberFormater, netPrice.blockchain.commission));
    writeCell(row, "net-usdc", format(numberFormater, netPrice.netUsdc));
    writeCell(row, "net-usdt", format(numberFormater, netPrice.netUsdt));
    writeCell(row, "net-dai", format(numberFormater, netPrice.netDai));
}

function format(formatter, value) {
    return (value) ? formatter.format(value) : "";
}

function writeCell(row, dataCryptoId, data) {
    const cell = row.querySelector(`[data-crypto-id = "${dataCryptoId}"]`);
    cell.textContent = data ?? "";
}