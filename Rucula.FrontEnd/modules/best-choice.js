export default function showBestChoice(winner, numberFormater) {
    const bestChoice = document.getElementById("mejor-opcion");
    bestChoice.innerHTML = `${winner.name} a $${numberFormater.format(winner.dolarPrice)}`;
}