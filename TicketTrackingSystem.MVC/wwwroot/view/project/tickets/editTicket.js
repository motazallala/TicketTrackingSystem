import { ticketTable } from './ticketTable.js';
import { setupModalData, showErrorModal } from '../../../utility/dataModalUtility.js';
import { updateTicketWithAutoStageAsync } from '../../../services/ticketServices.js';

$(document).ready(function () {
    // Getting the stage value
    let inStage = $('#inStage').val(); // Assuming this is already available in your DOM

    ticketTable.on('click', '.dt-edit', async function () {
        let data = ticketTable.row($(this).parents('tr')).data();

        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content with checkbox, message container and placeholder for closing message
        const title = 'Edit Ticket';
        const bodyContent = `
            <div class="form-group" id="isFinishedContainer">
                <input type="checkbox" id="isFinished" name="isFinished">
                <label for="isFinished">Is Finished</label>
            </div>
            <div class="form-group" id="messageContainer" style="display: none;">
                <label for="message">Message :</label>
                <textarea class="form-control" id="message" name="message"></textarea>
            </div>
            <div class="form-group" id="closeMessageContainer" style="display: none;">
                <p>Do you mark this as closed?</p>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const acceptButton = '<button type="button" class="btn btn-success" id="accept">Accept</button>';
        const rejectButton = '<button type="button" class="btn btn-danger" id="reject">Reject</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, acceptButton, rejectButton]);

        populateTicketForm(data);

        // Show modal
        $('#myModal').modal('show');

        // Handling based on the inStage value
        if (inStage === 'Stage 1 Tickets' || inStage === 'All Tickets') {
            // Show checkbox for Stage 1 Tickets or All Tickets
            $('#isFinishedContainer').show();

            // Initially hide message box and buttons until checkbox is checked
            $('#messageContainer').hide();
            $('#accept').hide();
            $('#reject').hide();

            // Toggle message textarea and buttons based on the isFinished checkbox
            $('#isFinished').on('change', function () {
                if ($(this).is(':checked')) {
                    $('#messageContainer').fadeIn(); // Show textarea
                    $('#accept').fadeIn().text('Accept'); // Show Accept button as "Accept"
                    $('#reject').fadeIn(); // Show Reject button
                } else {
                    $('#messageContainer').fadeOut(); // Hide textarea
                    $('#accept').fadeIn().text('Next'); // Hide Accept button
                    $('#reject').fadeOut(); // Hide Reject button
                }
            });
        } else if (inStage === 'Stage 2 Tickets') {
            // Hide checkbox, message box, and reject button for Stage 2 Tickets
            $('#isFinishedContainer').hide();
            $('#messageContainer').hide();
            $('#reject').hide();

            // Show the closing message and change Accept button to "Solved"
            $('#closeMessageContainer').fadeIn(); // Show "Do you mark this as closed?" message
            $('#accept').fadeIn().text('Solved'); // Change Accept button text to "Solved"
        }

        // Trigger the initial state of buttons and textarea when modal is loaded
        $('#isFinished').trigger('change');

        // Accept button click handler
        $('#accept').on('click', async function () {
            await handleUpdateTicket(data.id, 'accept');
        });

        // Reject button click handler
        $('#reject').on('click', async function () {
            await handleUpdateTicket(data.id, 'reject');
        });
    });

    // Gather form data function
    function gatherFormData(status) {
        return {
            ticketStatus: status, // 'accept' or 'reject'
            isFinished: $('#isFinished').is(':checked'), // Get checkbox value
            message: $('#message').val(), // Get textarea value
        };
    }

    // Function to update ticket on accept/reject
    async function handleUpdateTicket(ticketId, status) {
        const editTicketStatusDto = gatherFormData(status);

        const updateResult = await updateTicketWithAutoStageAsync(ticketId, editTicketStatusDto.ticketStatus, editTicketStatusDto.isFinished, editTicketStatusDto.message);

        try {
            if (updateResult.isSuccess) {
                $('.modal').modal('hide');
                ticketTable.ajax.reload(null, false); // Reload table data without resetting pagination
            } else {
                showErrorModal(updateResult.error.description);
            }
        } catch (e) {
            showErrorModal('There is an error that happened!');
        }
    }

    // Populate the form with existing data
    function populateTicketForm(data) {
        // Set the checkbox state based on the `isFinished` field from data
        if (data.isFinished) {
            $('#isFinished').prop('checked', true); // Check the checkbox
            $('#messageContainer').fadeIn(); // Show textarea if isFinished is true
        } else {
            $('#isFinished').prop('checked', false); // Uncheck the checkbox
            $('#messageContainer').fadeOut(); // Hide textarea if isFinished is false
        }

        // Trigger checkbox change event to update buttons and message field visibility
        $('#isFinished').trigger('change');
    }
});
