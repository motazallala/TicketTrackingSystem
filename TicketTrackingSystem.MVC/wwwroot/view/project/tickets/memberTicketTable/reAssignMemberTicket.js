import { memberTicketTable } from './memberTicketTable.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { getAllFreeMembersDropdownAsync, reAssignTicketAsync } from '../../../../services/ticketServices.js';

$(document).ready(async function () {
    let projectId = $('#projectId').val();

    // Load project members when page loads
    async function loadProjectMembers() {
        try {
            const result = await getAllFreeMembersDropdownAsync(projectId);
            if (result.isSuccess) {
                $('#projectMember').html(result.data);
            } else {
                showErrorModal('Failed to load project members.');
            }
        } catch (error) {
            console.error('Error loading project members:', error);
            showErrorModal('An error occurred while loading project members.');
        }
    }

    memberTicketTable.on('click', '.dt-reAssign', async function () {
        let data = memberTicketTable.row($(this).parents('tr')).data();

        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Re-Assign Ticket';
        const bodyContent = `
            <div class="form-group">
                <p>Are you sure you want to re-assign this ticket?</p>
            </div>
            <div class="form-group">
                <label for="projectMember">Project Members :</label>
                <select class="form-control" id="projectMember" name="projectMember"></select>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const reAssignButton = `<button type="button" class="btn btn-success" id="reAssignTicket">Re-Assign</button>`;

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, reAssignButton]);

        // Load members into dropdown after modal is shown
        $('#myModal').modal('show');
        await loadProjectMembers();

        // Assign button click handler
        $('#reAssignTicket').off('click').on('click', async function () {
            const selectedMemberId = $('#projectMember').val();
            if (!selectedMemberId) {
                showErrorModal('Please select a project member.');
                return;
            }
            await handleReAssignTicket(data.id, selectedMemberId);
        });
    });

    async function handleReAssignTicket(ticketId, selectedMemberId) {
        try {
            const assignResult = await reAssignTicketAsync(ticketId, selectedMemberId);

            if (assignResult.isSuccess) {
                $('#myModal').modal('hide');
                memberTicketTable.ajax.reload(null, false);
            } else {
                showErrorModal(assignResult.error.description);
            }
        } catch (error) {
            showErrorModal('An error occurred while assigning the ticket. Please try again.');
        }
    }
});