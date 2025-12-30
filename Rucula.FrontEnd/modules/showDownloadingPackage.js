let msgElement;

export function showDownloadingPackage(packageName) {
    msgElement = msgElement || document.getElementById("notify-progress");
    msgElement.textContent = packageName;
}
