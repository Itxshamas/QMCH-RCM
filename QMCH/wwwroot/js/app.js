// Utility function to trigger file input click
function triggerFileInput(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.click();
    }
}

// Download file function
function downloadFile(filename, content, contentType) {
    const blob = new Blob([content], { type: contentType });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
}

// Export for ES modules
export { triggerFileInput, downloadFile };

