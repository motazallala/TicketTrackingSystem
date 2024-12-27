import { roleTable } from './roleTable.js';
import { setupModalData } from '../../utility/dataModalUtility.js';
import { deleteRoleAsync } from '../../services/roleServices.js';

$(document).ready(function () {
    roleTable.on("click", '.dt-delete', async function () {
        const $this = $(this);
        const dtRow = $this.parents('tr');
        const data = roleTable.row(dtRow).data();

        //model elemants
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content
        const title = 'Delete Role';
        const bodyContent = `<p>Are you sure you want to delete Role ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteRole">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteRole').off('click').on('click', async function () {
            const roleName = data.name;
            const deleteResult = await deleteRoleAsync(roleName)
            if (deleteResult.isSuccess) {
                roleTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else {
                showErrorModal(deleteResult.error.description);
            }
        });
    });
});