export default function showBestChoice(winner, numberFormater) {
    const winnerName = document.getElementById("winner-name");
    winnerName.textContent = winner.name;

    const winnerPrice = document.getElementById("winner-price");
    winnerPrice.textContent = "$" + numberFormater.format(winner.dolarPrice);
}