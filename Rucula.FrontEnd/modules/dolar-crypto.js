export default function showDolarCrypto(dolarCrypto, numberFormater) {
    const dolarCryptoCompra = document.getElementById("dolar-crypto-compra");
    const dolarCryptoVenta = document.getElementById("dolar-crypto-venta");

    dolarCryptoCompra.textContent = numberFormater.format(dolarCrypto.precioCompra);
    dolarCryptoVenta.textContent = numberFormater.format(dolarCrypto.precioVenta);
}