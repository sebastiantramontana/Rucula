let msgElement;

export function showDownloadingPackageName(packageName) {
    msgElement = msgElement || document.getElementById("notify-progress");
    msgElement.textContent = packageName;
}

export function removeDownloadingPackagesIndicator() {
    const downloadingIndicator = document.getElementById("downloading-packages-indicator");
    downloadingIndicator.remove();
}
