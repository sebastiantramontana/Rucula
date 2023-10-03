export default async function showDolarCrypto()
{
    const dolarCrypto = await DotNet.invokeMethodAsync('Rucula.WebAssembly', 'GetDolarCrypto');
    fillDolarCrypto(dolarCrypto);
}

function fillDolarCrypto(dolarCrypto) {
    const dolarCryptoCompra = document.getElementById("dolar-crypto-compra");
    const dolarCryptoVenta = document.getElementById("dolar-crypto-venta");

    dolarCryptoCompra.innerHTML = dolarCrypto.precioCompra;
    dolarCryptoVenta.innerHTML = dolarCrypto.precioVenta;
}