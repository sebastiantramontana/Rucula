export default function showDolarWesternUnion(dolarWesternUnion, numberFormater) {
    showFinalPrice(numberFormater.format(dolarWesternUnion.netPrice));
    showGrossPrice(numberFormater.format(dolarWesternUnion.grossPrice));
    showFees(numberFormater.format(dolarWesternUnion.fees));
    showNetPrice(numberFormater.format(dolarWesternUnion.netPrice));
}

function showFinalPrice(price) {
    showValueInElement("dolar-western-union", price);
}

function showGrossPrice(price) {
    showValueInElement("gross-dolar-western-union", price);
}

function showFees(fees) {
    showValueInElement("gross-fees-western-union", fees);
}

function showNetPrice(price) {
    showValueInElement("net-dolar-western-union", price);
}

function showValueInElement(elementId, value) {
    const element = document.getElementById(elementId);
    element.textContent = value;
}
