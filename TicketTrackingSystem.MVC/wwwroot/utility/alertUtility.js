// Utility to create a Bootstrap alert
//<!-- Alert Container (should be somewhere in your HTML body) -->
//<div id="alertContainer" style="position: fixed; top: 20px; right: 20px; z-index: 1050;"></div>

export function createAlert(type, message, options = {}) {
    const { dismissible = true, autoDismiss = true, timeout = 5000 } = options;

    // Define icons for each alert type
    const icons = {
        success: '<i class="bi bi-check-circle-fill me-2"></i>',
        danger: '<i class="bi bi-exclamation-circle-fill me-2"></i>',
        warning: '<i class="bi bi-exclamation-triangle-fill me-2"></i>',
        info: '<i class="bi bi-info-circle-fill me-2"></i>'
    };

    // Create alert div
    const alertDiv = $('<div></div>')
        .addClass(`alert alert-${type} alert-dismissible fade show d-flex align-items-center`)
        .attr('role', 'alert');

    // Create the content div with icon and message
    const contentDiv = $('<div></div>').addClass('d-flex align-items-center');

    // Add icon
    contentDiv.append(icons[type] || '');

    // Add message
    contentDiv.append(message);

    // Add content and close button to alert
    alertDiv.append(contentDiv);
    alertDiv.append('<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');

    // Append alert to the container
    $('#alertContainer').append(alertDiv);

    // Initialize Bootstrap alert
    const bsAlert = new bootstrap.Alert(alertDiv[0]);

    // Handle auto-dismiss feature
    if (autoDismiss) {
        setTimeout(() => {
            bsAlert.close();
            // Remove the alert from DOM after animation
            alertDiv.on('closed.bs.alert', function () {
                $(this).remove();
            });
        }, timeout);
    }

    return alertDiv;
}

// Function to show a success alert
export function showSuccessAlert(message, options = {}) {
    return createAlert('success', message, options);
}

// Function to show a danger alert
export function showErrorAlert(message, options = {}) {
    return createAlert('danger', message, options);
}

// Function to show a warning alert
export function showWarningAlert(message, options = {}) {
    return createAlert('warning', message, options);
}

// Function to show an info alert
export function showInfoAlert(message, options = {}) {
    return createAlert('info', message, options);
}

export default {
    createAlert,
    showSuccessAlert,
    showErrorAlert,
    showWarningAlert,
    showInfoAlert
};