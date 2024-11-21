export default function showDolarCrypto(rankingCryptos, numberFormater) {
    const table = document.getElementById("tabla-crypto");
    const newTable = table.cloneNode(true);

    removePreviousTBodies(newTable);

    const tbodies = createExchangeBodies(rankingCryptos, numberFormater);
    highlightBestNetPrice(tbodies, rankingCryptos);

    tbodies.forEach(tbody => newTable.appendChild(tbody));
    table.replaceWith(newTable);
}

function highlightBestNetPrice(tbodies, rankingCryptos) {
    if (tbodies.length === 0)
        return;

    const firstNetPrices = rankingCryptos[0].dolarCryptoNetPrices[0];
    const bestNetColumnIndex = getBestNetColumnIndex(firstNetPrices);

    if (typeof bestNetColumnIndex === "number") {
        const firstNetPricesRow = tbodies[0].rows[1];
        highlightBestNetCell(firstNetPricesRow.cells[bestNetColumnIndex]);
    }
}

function getBestNetColumnIndex(firstNetPrices) {
    const topNetPrice = firstNetPrices.topNetPrice.netPrice;

    const netPrices = [
        { netPrice: firstNetPrices.netUsdc, netColumnIndex: 2 },
        { netPrice: firstNetPrices.netUsdt, netColumnIndex: 4 },
        { netPrice: firstNetPrices.netDai, netColumnIndex: 6 }
    ];

    return netPrices
        .find(p => IsTopNetPrice(p.netPrice, topNetPrice))
        .netColumnIndex;
}

function IsTopNetPrice(optionalNetPrice, topNetPrice) {
    return optionalNetPrice.hasValue && optionalNetPrice.value.netPrice === topNetPrice;
}

function highlightBestNetCell(cell) {
    const cssClasses = ["ring-2", "ring-inset", "ring-sky-500", "dark:ring-white/50"];
    const cssClassList = cell.classList;

    for (const className of cssClasses) {
        cssClassList.add(className);
    }
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
    let wasPreviousLastRowOdd = false;

    rankingCryptos.forEach(cryptoPrices => {
        const tbody = tbodyTemplate.cloneNode(true);

        setRowSpanToExchangeCells(tbody, cryptoPrices.dolarCryptoNetPrices, wasPreviousLastRowOdd);
        writeExchangeCells(tbody, cryptoPrices, numberFormater);
        addBlockchainRowsToTBody(tbody, cryptoPrices.dolarCryptoNetPrices, numberFormater);

        tbodies.push(tbody);
        wasPreviousLastRowOdd = isAlternatingColorForNextFirstRowNeeded(wasPreviousLastRowOdd, cryptoPrices.dolarCryptoNetPrices.length);
    });

    return tbodies;
}

function addBlockchainRowsToTBody(tbody, netPrices, numberFormater) {
    const rows = createBlockchainNetRows(netPrices, numberFormater);
    rows.forEach(row => tbody.appendChild(row));
}

function setRowSpanToExchangeCells(tbody, netPrices, needAlternatingColorFirstRow) {
    if (needAlternatingColorFirstRow)
        addHiddenBalancingAlternatingColorFirstRow(tbody);

    const rowspan = calculateRowSpanForExchangeCell(netPrices, needAlternatingColorFirstRow);
    setRowSpanAttributeToExchangeCells(tbody, rowspan);
}

function writeExchangeCells(tbody, cryptoPrices, numberFormater) {
    const firstRow = tbody.firstElementChild;

    writeCell(firstRow, "exchange", cryptoPrices.exchangeName);
    writeCell(firstRow, "gross-usdc", format(numberFormater, cryptoPrices.grossUsdc));
    writeCell(firstRow, "gross-usdt", format(numberFormater, cryptoPrices.grossUsdt));
    writeCell(firstRow, "gross-dai", format(numberFormater, cryptoPrices.grossDai));

}

function isAlternatingColorForNextFirstRowNeeded(wasPreviousLastRowOdd, rowsCount) {
    return wasPreviousLastRowOdd ^ isOdd(rowsCount);
}

function isOdd(number) {
    return number % 2 !== 0;
}

function calculateRowSpanForExchangeCell(netPrices, needAlternatingColorFirstRow) {
    var rowspan = netPrices.length + 1;
    return incrementRowspanForHiddenBalancingAlternatingColorFirstRow(rowspan, needAlternatingColorFirstRow);
}

function incrementRowspanForHiddenBalancingAlternatingColorFirstRow(rowspan, needAlternatingColorFirstRow) {
    if (needAlternatingColorFirstRow)
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