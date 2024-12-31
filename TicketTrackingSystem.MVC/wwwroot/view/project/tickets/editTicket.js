import { ticketTable } from './ticketTable.js';
import { setupModalData, showErrorModal } from '../../../utility/dataModalUtility.js';
import { getTicketStatusDropdown, updateTicketStatusWithAutoStageAsync } from '../../../services/ticketServices.js';

$(document).ready(function () {
    // Cache for dropdown options to avoid repeated server calls
    let cachedTicketStatus = null;

    ticketTable.on('click', '.dt-edit', async function () {
        let data = ticketTable.row($(this).parents('tr')).data();

        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Edit Ticket';
        const bodyContent = `
            <div class="form-group">
                <label for="ticketStatus">Ticket Status :</label>
                <select class="form-control" id="ticketStatus" name="ticketStatus"></select>
            </div>
            <div class="form-group">
                <input type="checkbox" id="isFinished" name="isFinished">
                <label for="isFinished">Is Finished</label>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const editTicketButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, editTicketButton]);

        populateProjectForm(data);

        // Show modal
        $('#myModal').modal('show');

        $('#submit').on('click', async function () {
            const editTicketStatusDto = gatherFormData();

            try {
                const updateResult = await updateTicketStatusWithAutoStageAsync(data.id, editTicketStatusDto.ticketStatus, editTicketStatusDto.isFinished);

                if (updateResult.isSuccess) {
                    $('.modal').modal('hide');
                    ticketTable.ajax.reload(null, false); // Reload table data without resetting pagination
                } else {
                    showErrorModal(updateResult.error.description);
                }
            } catch (e) {
                showErrorModal('An error occurred while updating the ticket status.');
            }
        });
    });

    function gatherFormData() {
        return {
            ticketStatus: $('#ticketStatus').val(),
            isFinished: $('#isFinished').is(':checked'), // Get checkbox value (true if checked)
        };
    }

    async function populateDropdowns() {
        if (!cachedTicketStatus) {
            cachedTicketStatus = await getTicketStatusDropdown();
        }
        if (cachedTicketStatus.isSuccess) {
            $('#ticketStatus').html(cachedTicketStatus.data);
        } else {
            showErrorModal(cachedTicketStatus.error.description);
        }
    }

    function populateProjectForm(data) {
        // Populate the ticketStatus dropdown
        populateDropdowns();

        // Set the checkbox state based on the `isFinished` field from data
        if (data.isFinished) {
            $('#isFinished').prop('checked', true); // Check the checkbox if `isFinished` is true
        } else {
            $('#isFinished').prop('checked', false); // Uncheck if `isFinished` is false
        }
    }
});
