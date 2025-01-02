import { ticketTable } from './ticketTable.js';
import { setupModalData, showErrorModal } from '../../../utility/dataModalUtility.js';
import { addTicketAsync } from '../../../services/ticketServices.js';
$(document).ready(function () {
    let projectId = $('#projectId').val()
    $("#addButton").on('click', async function () {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = 'Add Ticket';
        const bodyContent = `
            <div class="form-group my-1">
                <label for="title">Title :</label>
                <input type="text" class="form-control" id="title" name="title">
            </div>
            <div class="form-group my-1">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="description" name="description">
            </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);
        $('#myModal').modal('show');
        $('#submit').off('click').on('click', async function () {

            try {
                const dto = gatherFormData();
                //call the addTicketAsync method from the ticketServices
                const addResult = await addTicketAsync(dto)
                if (addResult.isSuccess) {
                    //close the modal
                    $('.modal').modal('hide');
                    //reload the ticketTable
                    ticketTable.ajax.reload(null, false);

                }
                else {
                    showErrorModal(addResult.error.description);
                }

            } catch (e) {
                showErrorModal('An error occurred while loading the data.');
            }
        });
    });

    function gatherFormData() {
        return {
            title: $('#title').val(),
            description: $('#description').val(),
            projectId: projectId
        };
    }
});
