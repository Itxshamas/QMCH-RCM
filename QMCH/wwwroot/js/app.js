// Utility function to trigger file input click
function triggerFileInput(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.click();
    }
}

// Export for ES modules
export { triggerFileInput };
