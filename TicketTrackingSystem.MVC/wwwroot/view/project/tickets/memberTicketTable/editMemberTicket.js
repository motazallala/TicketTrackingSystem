import { memberTicketTable } from './memberTicketTable.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { updateTicketWithAutoStageAsync } from '../../../../services/ticketServices.js';

$(document).ready(function () {
    let inStage = $('#inStage').val() || 'All Tickets'; // Add default value

    $(document).on('click', '.dt-edit', async function () {
        const row = memberTicketTable.row($(this).closest('tr'));
        if (!row) return;
        
        const data = row.data();
        if (!data) {
            showErrorModal('Could not retrieve ticket data.');
            return;
        }

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
              <label class="form-check-label" for="isFinished">Are This Ticket Finished Reviewing And You Whant To Accept - Reject ?</label>
            </div>
            <div class="form-group" id="messageContainer">
                <label for="message" id="messageLabel">Message :</label>
                <textarea class="form-control" id="message" name="message" style="height: 200px;"></textarea>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const acceptButton = `<button type="button" class="btn btn-success" id="accept">Accept</button>`;
        const rejectButton = `<button type="button" class="btn btn-danger" id="reject">Reject</button>`;
        const returnButton = `<button type="button" class="btn btn-warning" id="returnTicket" style="display: none;">Returned</button>`;

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, acceptButton, rejectButton, returnButton]);

        populateTicketForm(data);

        $('#myModal').modal('show');

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
                        $('#reject').fadeIn(); // Show Reject button
                    }
                    $('#messageLabel').html('<strong>Message For The Client :</strong>');
                } else {
                    $('#accept').fadeIn().text('Next');
                    $('#reject').fadeOut();
                    $('#messageLabel').html('<strong>Message For The Next Stage :</strong>');
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

        // Event handlers
        $('#accept').on('click', async function () {
            if (data.id) {
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
    });

    function gatherFormData(status) {
        return {
            ticketStatus: status,
            isFinished: $('#isFinished').is(':checked'),
            message: $('#message').val() || ''  // Ensure message is never undefined
        };
    }

    async function handleUpdateTicket(ticketId, status) {
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
                editTicketStatusDto.message
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

        // Safely set the checkbox state
        const isFinished = data.isFinished || false;
        $('#isFinished').prop('checked', isFinished);

        // If there's a message field in the data, populate it
        if (data.message) {
            $('#message').val(data.message);
        }

        // Trigger change event to update UI state
        $('#isFinished').trigger('change');
    }
});