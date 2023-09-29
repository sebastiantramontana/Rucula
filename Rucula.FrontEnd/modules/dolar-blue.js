export default async function showDolarBlue()
{
    const dolarBlue = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetDolarBlue');
    fillDolarBlue(dolarBlue);
}

function fillDolarBlue(dolarBlue) {
    const dolarBlueCompra = document.getElementById("dolar-blue-compra");
    const dolarBlueVenta = document.getElementById("dolar-blue-venta");

    dolarBlueCompra.innerHTML = dolarBlue.precioCompra;
    dolarBlueVenta.innerHTML = dolarBlue.precioVenta;
}