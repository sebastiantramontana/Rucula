export default function showDolarBlue(dolarBlue)
{
    const dolarBlueCompra = document.getElementById("dolar-blue-compra");
    const dolarBlueVenta = document.getElementById("dolar-blue-venta");

    dolarBlueCompra.textContent = dolarBlue.precioCompra;
    dolarBlueVenta.textContent = dolarBlue.precioVenta;
}