import { memberTicketTable } from './memberTicketTable.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { assignTicketToUserAsync } from '../../../../services/ticketServices.js';

$(document).ready(function () {
    // Handling "Assign to Me" action (assuming `reserved` is false in this case)
    memberTicketTable.on('click', '.dt-assign', function () {
        let data = memberTicketTable.row($(this).parents('tr')).data(); // Get row data

        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content with a note and comment field
        const title = 'Assign Ticket to Me';
        const bodyContent = `
            <div class="form-group">
                <p>Are you sure you want to assign this ticket to yourself?</p>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const assignButton = `<button type="button" class="btn btn-success" id="assignTicket">Assign to Me</button>`;

        // Setup modal with title, content, and buttons
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, assignButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Assign button click handler
        $('#assignTicket').on('click', async function () {

            // Call the function to handle assignment
            await handleAssignTicket(data.id);
        });
    });

    // Function to handle ticket assignment
    async function handleAssignTicket(ticketId) {
        try {
            // Placeholder backend request for assigning the ticket
            // Replace this with your actual logic for assigning a ticket
            const assignResult = await assignTicketToUserAsync(ticketId);

            if (assignResult.isSuccess) {
                // Close the modal and reload the DataTable
                $('#myModal').modal('hide');
                memberTicketTable.ajax.reload(null, false); // Reload table without resetting pagination
            } else {
                // Show error if assignment fails
                showErrorModal(assignResult.error.description);
            }
        } catch (error) {
            // Handle any unexpected errors
            showErrorModal('An error occurred while assigning the ticket. Please try again.');
        }
    }
});
