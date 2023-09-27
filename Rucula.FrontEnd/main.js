import fillTitulosTable from "./modules/titulos-table.js";

addEventListener("load", async () => {
    await Blazor.start();

    const dolarBlue = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetDolarBlue');
    ShowDolarBlue(dolarBlue);

    const titulos = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    fillTitulosTable(titulos);
});

function ShowDolarBlue(dolarBlue) {
    const dolarBlueCompra = document.getElementById("dolar-blue-compra");
    const dolarBlueVenta = document.getElementById("dolar-blue-venta");

    dolarBlueCompra.innerHTML = dolarBlue.precioCompra;
    dolarBlueVenta.innerHTML = dolarBlue.precioVenta;
}
