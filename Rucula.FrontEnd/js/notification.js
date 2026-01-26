let msgElement;

export function notifyMessage(message) {
    msgElement = msgElement || document.getElementById("notify-progress");
    msgElement.textContent = message;
}

export function removeDownloadingPackagesIndicator() {
    const downloadingIndicator = document.getElementById("downloading-packages-indicator");
    downloadingIndicator.remove();
}
