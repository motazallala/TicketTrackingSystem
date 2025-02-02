// spinner.js
/**
 * Shows a Bootstrap spinner in the specified element
 * @param {string} elementId - ID of the container element
 */
export function showSpinner(elementId) {
    const container = document.getElementById(elementId);
    if (!container) return;

    // Clear existing content
    container.innerHTML = '';

    // Create spinner element
    const spinner = document.createElement('div');
    spinner.className = 'spinner-border spinner-border-sm text-secondary';
    spinner.setAttribute('role', 'status');

    // Create screen reader-only text
    const srText = document.createElement('span');
    srText.className = 'visually-hidden';
    srText.textContent = 'Loading...';

    // Assemble and append to container
    spinner.appendChild(srText);
    container.appendChild(spinner);
}

/**
 * Hides the spinner in the specified element
 * @param {string} elementId - ID of the container element
 */
export function hideSpinner(elementId) {
    const container = document.getElementById(elementId);
    if (!container) return;

    // Remove any existing spinner
    const spinner = container.querySelector('.spinner-border');
    if (spinner) {
        container.removeChild(spinner);
    }
}