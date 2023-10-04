export default async function showDolarBlue(dolarBlue)
{
    const dolarBlueCompra = document.getElementById("dolar-blue-compra");
    const dolarBlueVenta = document.getElementById("dolar-blue-venta");

    dolarBlueCompra.innerHTML = dolarBlue.precioCompra;
    dolarBlueVenta.innerHTML = dolarBlue.precioVenta;
}