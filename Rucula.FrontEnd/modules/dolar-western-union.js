export default function showDolarWesternUnion(dolarWesternUnion, numberFormater) {

    const formatedValueFunc = getFormatedValueFunc(dolarWesternUnion, numberFormater);

    showFinalPrice(formatedValueFunc("netPrice"));
    showGrossPrice(formatedValueFunc("grossPrice"));
    showFees(formatedValueFunc("fees"));
    showNetPrice(formatedValueFunc("netPrice"));
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

const getFormatedValueFunc = (dolarWesternUnion, numberFormater) =>
    dolarWesternUnion.hasValue
        ? (propertyKey) => numberFormater.format(dolarWesternUnion.value[propertyKey])
        : () => "-";