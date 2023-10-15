export default function showDolarCrypto(dolarCrypto)
{
    const dolarCryptoCompra = document.getElementById("dolar-crypto-compra");
    const dolarCryptoVenta = document.getElementById("dolar-crypto-venta");

    dolarCryptoCompra.innerHTML = dolarCrypto.precioCompra;
    dolarCryptoVenta.innerHTML = dolarCrypto.precioVenta;
}