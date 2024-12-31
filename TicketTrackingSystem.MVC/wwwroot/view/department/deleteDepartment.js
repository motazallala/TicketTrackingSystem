import { departmentTable } from './departmentTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { deleteDepartmentAsync } from '../../services/departmentServices.js';

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
        const title = 'Delete Department';
        const bodyContent = `<p>Are you sure you want to delete department ${data.name}?</p>`;
        const deleteButton = `<button class="btn btn-danger" data-id="${data.id}" id="deleteDepartment">Delete</button>`;
        const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

        // Set up the modal
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

        // Show the modal
        $('#myModal').modal('show');

        // Event listener for the delete button inside the modal
        $('#deleteDepartment').on('click', async function () {
            const departmentId = data.id;
            const deleteResult = await deleteDepartmentAsync(departmentId)
            if (deleteResult.isSuccess) {
                departmentTable.ajax.reload();
                $('#myModal').modal('hide');
            }
            else {
                showErrorModal(deleteResult.error.description);
            }
        });
    });
});