export default function showDolarCrypto(rankingCryptos, numberFormater) {
    const table = document.getElementById("tabla-crypto");
    const newTable = table.cloneNode(true);

    removePreviousTBodies(newTable);
    const tbodies = createExchangeBodies(rankingCryptos, numberFormater);
    tbodies.forEach(tbody => newTable.appendChild(tbody));

    table.replaceWith(newTable);
}

function removePreviousTBodies(table) {
    const count = table.tBodies.length;

    for (let i = 0; i < count; i++) {
        table.removeChild(table.tBodies[0]);
    };
}

function createExchangeBodies(rankingCryptos, numberFormater) {

    const tbodies = [];
    const tbodyTemplate = document.getElementById("crypto-exchange-tbody-template").content.querySelector("tbody");
    let arePreviousRowsOdd = false;

    rankingCryptos.forEach(cryptoPrices => {
        const tbody = tbodyTemplate.cloneNode(true);

        setRowSpanToExchangeCells(tbody, cryptoPrices.dolarCryptoNetPrices, arePreviousRowsOdd);
        writeExchangeCells(tbody, cryptoPrices, numberFormater);
        addBlockchainRowsToTBody(tbody, cryptoPrices.dolarCryptoNetPrices, numberFormater);

        tbodies.push(tbody);
        arePreviousRowsOdd = isOdd(cryptoPrices.dolarCryptoNetPrices.length);
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

function writeExchangeCells(tbody, cryptoPrices, numberFormater) {
    const firstRow = tbody.firstElementChild;

    writeCell(firstRow, "exchange", cryptoPrices.exchangeName);
    writeCell(firstRow, "gross-usdc", format(numberFormater, cryptoPrices.grossUsdc));
    writeCell(firstRow, "gross-usdt", format(numberFormater, cryptoPrices.grossUsdt));
    writeCell(firstRow, "gross-dai", format(numberFormater, cryptoPrices.grossDai));

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

function writeBlockchainCells(row, netPrices, numberFormater) {
    writeCell(row, "blockchain-name", netPrices.blockchain.name);
    writeCell(row, "usdc-fee", formatObject(numberFormater, netPrices.netUsdc, v => v.fee));
    writeCell(row, "net-usdc", formatObject(numberFormater, netPrices.netUsdc, v => v.netPrice));
    writeCell(row, "usdt-fee", formatObject(numberFormater, netPrices.netUsdt, v => v.fee));
    writeCell(row, "net-usdt", formatObject(numberFormater, netPrices.netUsdt, v => v.netPrice));
    writeCell(row, "dai-fee", formatObject(numberFormater, netPrices.netDai, v => v.fee));
    writeCell(row, "net-dai", formatObject(numberFormater, netPrices.netDai, v => v.netPrice));
}

function format(formatter, optional) {
    return (optional && optional.hasValue) ? formatter.format(optional.value) : "";
}

function formatObject(formatter, optional, valueCallback) {
    return (optional && optional.hasValue) ? formatter.format(valueCallback(optional.value)) : "";
}

function writeCell(row, dataCryptoId, data) {
    const cell = row.querySelector(`[data-crypto-id = "${dataCryptoId}"]`);
    cell.textContent = data ?? "";
}