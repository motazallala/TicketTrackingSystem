/**
 * Shows a Bootstrap spinner in the specified element
 * @param {string} elementId - ID of the container element
 * @param {boolean} clearContent - Whether to clear existing content or not
 * @param {boolean} isOverlay - Whether the spinner should overlay the content or not
*/
export function showSpinner(elementId, clearContent = true, isSmall = true, isOverlay = false) {
    const container = document.getElementById(elementId);
    if (!container) return;

    // Clear existing content if specified
    if (clearContent) {
        container.innerHTML = '';
    }

    // Create spinner wrapper (for overlay or inline placement)
    const spinnerWrapper = document.createElement('div');

    if (isOverlay) {
        // If overlay, ensure container has relative positioning
        container.style.position = 'relative';

        // Set overlay-specific styles
        spinnerWrapper.style.position = 'absolute';
        spinnerWrapper.style.top = '50%';
        spinnerWrapper.style.left = '50%';
        spinnerWrapper.style.transform = 'translate(-50%, -50%)';
        spinnerWrapper.style.width = '100%';
        spinnerWrapper.style.height = '100%';
        spinnerWrapper.style.backgroundColor = 'rgba(0, 0, 0, 0.5)'; // Semi-transparent background
        spinnerWrapper.style.display = 'flex';
        spinnerWrapper.style.alignItems = 'center';
        spinnerWrapper.style.justifyContent = 'center';
        spinnerWrapper.style.zIndex = '1000'; // Ensure spinner is on top
    }

    // Create spinner element
    const spinner = document.createElement('div');
    spinner.className = `spinner-border ${isSmall ? "spinner-border-sm":""} text-secondary`;
    spinner.setAttribute('role', 'status');

    // Create screen reader-only text
    const srText = document.createElement('span');
    srText.className = 'visually-hidden';
    srText.textContent = 'Loading...';

    // Append spinner to spinner wrapper
    spinner.appendChild(srText);
    spinnerWrapper.appendChild(spinner);

    // Append spinner wrapper to container
    container.appendChild(spinnerWrapper);
}


/**
 * Hides the spinner in the specified element
 * @param {string} elementId - ID of the container element
 */
export function hideSpinner(elementId) {
    const container = document.getElementById(elementId);
    if (!container) return;

    // Remove any existing spinner
    const spinner = container.querySelector('.spinner-border').parentElement; // Ensure it removes the wrapper
    if (spinner) {
        container.removeChild(spinner);
    }
}


/**
 * Shows a global spinner that covers the entire page
 */
export function showGlobalSpinner() {
    // Create a global spinner container if it doesn't exist
    let globalSpinnerContainer = document.getElementById('global-spinner-container');
    if (!globalSpinnerContainer) {
        globalSpinnerContainer = document.createElement('div');
        globalSpinnerContainer.id = 'global-spinner-container';
        globalSpinnerContainer.style.position = 'fixed';
        globalSpinnerContainer.style.top = '0';
        globalSpinnerContainer.style.left = '0';
        globalSpinnerContainer.style.width = '100%';
        globalSpinnerContainer.style.height = '100%';
        globalSpinnerContainer.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
        globalSpinnerContainer.style.display = 'flex';
        globalSpinnerContainer.style.justifyContent = 'center';
        globalSpinnerContainer.style.alignItems = 'center';
        globalSpinnerContainer.style.zIndex = '9999';

        // Create spinner element
        const spinner = document.createElement('div');
        spinner.className = 'spinner-border spinner-border-lg text-light';
        spinner.setAttribute('role', 'status');

        // Create screen reader-only text
        const srText = document.createElement('span');
        srText.className = 'visually-hidden';
        srText.textContent = 'Loading...';

        spinner.appendChild(srText);
        globalSpinnerContainer.appendChild(spinner);

        // Append the global spinner container to the body
        document.body.appendChild(globalSpinnerContainer);
    }
}

/**
 * Hides the global spinner
 */
export function hideGlobalSpinner() {
    const globalSpinnerContainer = document.getElementById('global-spinner-container');
    if (globalSpinnerContainer) {
        document.body.removeChild(globalSpinnerContainer);
    }
}
