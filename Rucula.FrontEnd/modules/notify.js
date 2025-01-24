let msgElement;

export function notify(message) {
    msgElement = msgElement || document.getElementById("notify-progress");
    msgElement.textContent = message;
}
