import {
    getTotalMemberTicketAsync,
    getTotalTicketsAsync,
    getTotalUsersAsync,
    getTotalClientTicketAsync
} from '../../services/dashboardServices.js';
import { showErrorModal } from '../../utility/dataModalUtility.js';
import { hideSpinner, showSpinner } from '../../utility/spinnerUtility.js';

// Generic data fetching and display handler
async function handleDataFetch(elementId, apiCall, label) {
    const element = $(`#${elementId}`);
    try {
        showSpinner(elementId);
        const result = await apiCall();

        if (result.isSuccess) {
            element.html(`${result.data} ${label}`);
        } else {
            showErrorModal(result.error.description);
            element.html('-'); // Show empty state
        }
    } catch (error) {
        showErrorModal(`Failed to load ${label.toLowerCase()} data`);
        element.html('-'); // Show empty state
    } finally {
        hideSpinner(elementId);
    }
}

$(document).ready(async () => {
    try {
        // Parallel execution of all data fetches
        await Promise.allSettled([
            handleDataFetch('totalUserNumber', getTotalUsersAsync, 'Users'),
            handleDataFetch('allTicketNumber', getTotalTicketsAsync, 'Tickets'),
            handleDataFetch('assignTicketNumber', getTotalMemberTicketAsync, 'Tickets'),
            handleDataFetch('openedTicket', getTotalClientTicketAsync, 'Tickets')
        ]);
    } catch (error) {
        showErrorModal("A critical error occurred during initialization");
    }
});