import { projectTable } from './projectTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { deleteProjectAsync } from '../../services/projectServices.js';

$(document).ready(function () {
    projectTable.on("click", '.dt-delete', async function () {
        const $this = $(this);
        const dtRow = $this.parents('tr');
        const data = projectTable.row(dtRow).data();

        //model elemants
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content
        const title = 'Delete Project';
        const bodyContent = `<p>Are you sure you want to delete Project ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteBtn">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteBtn').off('click').on('click', async function () {
            const projectId = data.id;
            const deleteResult = await deleteProjectAsync(projectId)
            if (deleteResult.isSuccess) {
                projectTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else {
                showErrorModal(deleteResult.error.description);
            }
        });
    });
});