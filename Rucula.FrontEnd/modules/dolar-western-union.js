export default async function showDolarWesternUnion()
{
    const dolarWesternUnion = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetDolarWesternUnion');
    fillDolarWesternUnion(dolarWesternUnion);
}

function fillDolarWesternUnion(dolarWesternUnion) {
    const dolarWesternUnionElement = document.getElementById("dolar-western-union");
    dolarWesternUnionElement.innerHTML = dolarWesternUnion.value;
}