export default function showDolarWesternUnion(dolarWesternUnion) {
    const dolarWesternUnionElement = document.getElementById("dolar-western-union");
    dolarWesternUnionElement.textContent = dolarWesternUnion.netPrice;
}
