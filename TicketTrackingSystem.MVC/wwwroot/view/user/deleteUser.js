import { userTable } from './userTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { deleteUserAsync } from '../../services/userServices.js';

$(document).ready(function () {
    userTable.on('click', '.dt-delete', async function () {
        const $this = $(this);
        const dtRow = $this.parents('tr');
        const data = userTable.row(dtRow).data();

        //model elemants
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content
        const title = 'Delete User';
        const bodyContent = `<p>Are you sure you want to delete user ${data.userName}?</p>`;
        const deleteButton = `<button class="btn btn-danger"  id="deleteDepartment">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteDepartment').off('click').on('click', async function () {
            const userId = data.id;
            const deleteResult = await deleteUserAsync(userId);

            if (deleteResult.isSuccess) {
                userTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else {
                showErrorModal(deleteResult.error.description);
            }

        });
    });
});