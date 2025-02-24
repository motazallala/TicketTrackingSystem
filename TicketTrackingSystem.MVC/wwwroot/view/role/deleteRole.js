import { roleTable } from './roleTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { deleteRoleAsync, deleteRoleCascadeAsync } from '../../services/roleServices.js';
import { showGlobalSpinner, hideGlobalSpinner } from '../../utility/spinnerUtility.js';

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
        let title = 'Delete Role';
        let bodyContent = `<p>Are You Sure You Want To Delete Role : ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteRole">Delete</button>`;
        const deleteButtonCascade = `<button class="btn btn-danger"  id="deleteUserWithCascade">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteRole').off('click').on('click', async function () {
            const roleName = data.name;
            showGlobalSpinner();
            const deleteResult = await deleteRoleAsync(roleName);
            hideGlobalSpinner();
            if (deleteResult.isSuccess) {
                roleTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else if (deleteResult.error.description === 'This Role Has Users') {
                title = 'Delete Role With Cascade';
                bodyContent = `<p>Are You Sure You Want To Delete Role : ${data.name} With Associated User In The Role?</p>`;
                setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButtonCascade, cancelButton]);
                // Event listener for the delete button inside the modal
                $('#deleteUserWithCascade').off('click').on('click', async function () {
                    const deleteResult = await deleteRoleCascadeAsync(roleName);

                    if (deleteResult.isSuccess) {
                        roleTable.ajax.reload();
                        $('#myModal').modal('hide');
                    }
                    else {
                        showErrorModal(deleteResult.error.description);
                    }

                });
            }
            else {
                showErrorModal(deleteResult.error.description);

            }
        });
    });
});