export default function showDolarWesternUnion(dolarWesternUnion) {
    const dolarWesternUnionElement = document.getElementById("dolar-western-union");
    dolarWesternUnionElement.innerHTML = dolarWesternUnion.price;
}
