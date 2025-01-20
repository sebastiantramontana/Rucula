export default function showDolarBlue(dolarBlue) {
    const dolarBlueCompra = document.getElementById("dolar-blue-compra");
    const dolarBlueVenta = document.getElementById("dolar-blue-venta");

    dolarBlueCompra.textContent = getValueOrInvalidString(dolarBlue, "precioCompra");
    dolarBlueVenta.textContent = getValueOrInvalidString(dolarBlue, "precioVenta");
}

const getValueOrInvalidString = (dolarBlue, propertyKey) =>
    dolarBlue.hasValue ? dolarBlue.value[propertyKey] : "-";
