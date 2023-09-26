import fillTitulosTable from "./modules/titulos-table.js";

addEventListener("load", async () => {
    await Blazor.start();

    const titulos = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    const tabla = document.getElementById("tabla-titulos");

    fillTitulosTable(tabla, titulos);
});
