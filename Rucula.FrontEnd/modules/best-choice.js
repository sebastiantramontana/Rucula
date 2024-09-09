export default function showBestChoice(winner, numberFormater) {
    const bestChoice = document.getElementById("mejor-opcion");
    bestChoice.textContent = `${winner.name} a $${numberFormater.format(winner.dolarPrice)}`;
}