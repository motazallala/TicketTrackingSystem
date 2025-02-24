import { departmentTable } from './departmentTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { showSuccessAlert } from '../../utility/alertUtility.js';
import { showGlobalSpinner, hideGlobalSpinner } from '../../utility/spinnerUtility.js';
import { deleteDepartmentAsync, deleteDepartmentCascadeAsync } from '../../services/departmentServices.js';

$(document).ready(function () {
    departmentTable.on('click', '.dt-delete', async function () {
        const $this = $(this);
        const dtRow = $this.parents('tr');
        const data = departmentTable.row(dtRow).data();

        //model elemants
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        // Modal content
        let title = 'Delete Department';
        let bodyContent = `<p>Are you sure you want to delete department ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteDepartment">Delete</button>`;
        const deleteButtonCascade = `<button class="btn btn-danger"  id="deleteWithCascade">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteDepartment').on('click', async function () {
            const departmentId = data.id;
            showGlobalSpinner();
            const deleteResult = await deleteDepartmentAsync(departmentId);
            hideGlobalSpinner();
            if (deleteResult.isSuccess) {
                showSuccessAlert(deleteResult.successMessage);
                departmentTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else if (deleteResult.error.description === 'There Is Existing Users In This Department.') {
                title = 'Delete Role With Cascade';
                bodyContent = `
                <p class='mb-2'>There Is Existing Users In This Department.</p>

                <p>Are You Sure You Want To Delete Department : ${data.name}?</p>`;
                setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButtonCascade, cancelButton]);
                $('#deleteWithCascade').off('click').on('click', async function () {
                    const deleteResult = await deleteDepartmentCascadeAsync(departmentId);

                    if (deleteResult.isSuccess) {
                        showSuccessAlert(deleteResult.successMessage);
                        departmentTable.ajax.reload();
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