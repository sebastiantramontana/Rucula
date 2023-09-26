import fillTitulosTable from "./modules/titulos-table.js";

addEventListener("load", async () => {
    await Blazor.start();

    let titulos = [1, 2, 3, 4];
    //titulos = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetTitulosRanking');
    const tabla = document.getElementById("tabla-titulos");

    fillTitulosTable(tabla, titulos);
});
