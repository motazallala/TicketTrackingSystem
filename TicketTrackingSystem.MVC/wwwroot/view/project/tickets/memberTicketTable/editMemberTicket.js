﻿import { memberTicketTable } from './memberTicketTable.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { updateTicketWithAutoStageAsync, checkEstimatedCompletionDateAsync, setEstimatedCompletionDateForReassignTicketAsync } from '../../../../services/ticketServices.js';

$(document).ready(function () {
    let inStage = $('#inStage').val() || 'All Tickets';

    function showEditModal(data) {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Edit Ticket';
        const bodyContent = `
            <div class="form-group" id="closeMessageContainer" style="display: none;">
                <p>You can describe the fix or the notes on this ticket.</p>
            </div>
            <div class="form-check form-switch" id="isFinishedContainer">
                <input class="form-check-input" type="checkbox" role="switch" id="isFinished" name="isFinished">
                <label class="form-check-label" for="isFinished">Is This Ticket Finished Reviewing And You Want To Accept/Reject?</label>
            </div>
            <div class="form-group" id="messageContainer">
                <label for="message" id="messageLabel">Message:</label>
                <textarea class="form-control" id="message" name="message" style="height: 200px;"></textarea>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>`;
        const acceptButton = `<button type="button" class="btn btn-success" id="accept">Accept</button>`;
        const rejectButton = `<button type="button" class="btn btn-danger" id="reject">Reject</button>`;
        const returnButton = `<button type="button" class="btn btn-warning" id="returnTicket" style="display: none;">Returned</button>`;

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, acceptButton, rejectButton, returnButton]);

        setupModalHandlers(data);
        populateTicketForm(data);

        $('#myModal').modal('show');
    }

    function showDateTimeModal(ticketId) {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Schedule Ticket Processing';
        const bodyContent = `
            <div class="form-group">
                <label for="scheduledDateTime">Select Date and Time:</label>
                <input type="datetime-local" class="form-control" id="scheduledDateTime" name="scheduledDateTime">
            </div>
        `;

        const closeButton = `<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>`;
        const confirmButton = `<button type="button" class="btn btn-primary" id="confirmDateTime">Confirm</button>`;

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, confirmButton]);

        // Handle datetime confirmation
        $('#confirmDateTime').off('click').on('click', async function () {
            const scheduledDateTime = $('#scheduledDateTime').val();
            if (!scheduledDateTime) {
                showErrorModal('Please select a date and time.');
                return;
            }

            let result = await setEstimatedCompletionDateForReassignTicketAsync(ticketId, scheduledDateTime);
            if (result.isSuccess) {
                $('#myModal').modal('hide');
            }
            else {
                showErrorModal(result?.error?.description || 'Update failed.');
            }
        });

        $('#myModal').modal('show');
    }

    function setupModalHandlers(data) {
        // Clear any existing event handlers
        $('#isFinished').off('change');
        $('#accept').off('click');
        $('#reject').off('click');
        $('#returnTicket').off('click');

        if (inStage === 'Stage 1 Tickets' || inStage === 'All Tickets') {
            $('#isFinishedContainer').show();
            $('#accept').hide();
            $('#reject').hide();
            $('#returnTicket').hide();

            $('#isFinished').on('change', function () {
                if ($(this).is(':checked')) {
                    $('#accept').fadeIn().text('Accept');
                    if (data.status !== 'Returned') {
                        $('#reject').fadeIn();
                    }
                    $('#messageLabel').html('<strong>Message For The Client:</strong>');
                } else {
                    $('#accept').fadeIn().text('Next');
                    $('#reject').fadeOut();
                    $('#messageLabel').html('<strong>Message For The Next Stage:</strong>');
                }
            });
        } else if (inStage === 'Stage 2 Tickets') {
            $('#isFinishedContainer').hide();
            $('#reject').hide();
            $('#closeMessageContainer').fadeIn();
            $('#accept').fadeIn().text('Solved');
            $('#returnTicket').fadeIn();
        }

        $('#isFinished').trigger('change');

        // Set up button handlers
        $('#accept').on('click', async function () {
            if (data.id) {
                $('#myModal').modal('hide');
                await handleUpdateTicket(data.id, 'accept');
            }
        });

        $('#reject').on('click', async function () {
            if (data.id) {
                await handleUpdateTicket(data.id, 'reject');
            }
        });

        $('#returnTicket').on('click', async function () {
            if (data.id) {
                await handleUpdateTicket(data.id, 'returned');
            }
        });
    }

    function gatherFormData(status) {
        let isFinishedChecked = false;
        if (inStage === 'Stage 2 Tickets' && status ==='accept') {
            isFinishedChecked = true;
        }
        else {
            isFinishedChecked = $('#isFinished').is(':checked');
        }
        const messageText = $('#message').val() || '';

        return {
            ticketStatus: status,
            isFinished: isFinishedChecked,
            message: messageText
        };
    }

    async function handleUpdateTicket(ticketId, status, scheduledDateTime = null) {
        try {
            const editTicketStatusDto = gatherFormData(status);

            if (!ticketId) {
                showErrorModal('Invalid ticket ID.');
                return;
            }

            const updateResult = await updateTicketWithAutoStageAsync(
                ticketId,
                editTicketStatusDto.ticketStatus,
                editTicketStatusDto.isFinished,
                editTicketStatusDto.message,
                scheduledDateTime
            );

            if (updateResult && updateResult.isSuccess) {
                $('#myModal').modal('hide');
                if (memberTicketTable && memberTicketTable.ajax) {
                    memberTicketTable.ajax.reload(null, false);
                }
            } else {
                showErrorModal(updateResult?.error?.description || 'Update failed.');
            }
        } catch (e) {
            showErrorModal('An error occurred during the update.');
            console.error('Update error:', e);
        }
    }

    function populateTicketForm(data) {
        if (!data) return;

        const isFinished = data.isFinished || false;
        $('#isFinished').prop('checked', isFinished);

        if (data.message) {
            $('#message').val(data.message);
        }

        $('#isFinished').trigger('change');
    }

    // Main click handler for edit button
    $(document).on('click', '.dt-edit', async function () {
        const row = memberTicketTable.row($(this).closest('tr'));
        if (!row) return;

        const data = row.data();
        if (!data) {
            showErrorModal('Could not retrieve ticket data.');
            return;
        }

        // Check ticket availability
        const result = await checkEstimatedCompletionDateAsync(data.id);

        if (result.isSuccess) {
            if (result.data === false) {
                showDateTimeModal(data.id);
                return;
            }
        }

        showEditModal(data);
    });
});