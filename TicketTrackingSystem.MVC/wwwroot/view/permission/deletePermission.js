import { removeRoleFromPermissionAsync } from '../../services/permissionServices.js';
import { roleTable } from './roleForPermissionTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';

$(document).on('click', '.permission-badge', async function () {
    const roleId = $(this).data('role-id'); // Get the role ID
    const permissionId = $(this).data('permission-id'); // Get the permission ID
    const permissionName = $(this).text(); // Get the permission name

    // Fetch the row data for the role
    const row = roleTable.row($(this).closest('tr')).data(); // Get the row data
    const roleName = row.name; // Extract the role name from the row data

    // Debugging
    console.log(`Role ID: ${roleId}, Role Name: ${roleName}, Permission ID: ${permissionId}, Permission Name: ${permissionName}`);

    // Modal elements
    const modalTitle = $('.modal .modal-title');
    const modalBody = $('#modelBody');
    const modalFooter = $('.modal .modal-footer');

    // Modal content
    const title = 'Delete Permission';
    const bodyContent = `
        <p>Are you sure you want to delete Permission <strong>${permissionName}</strong> 
        from the Role <strong>${roleName}</strong>?</p>`;
    const deleteButton = `<button class="btn btn-danger" id="deletePermission">Delete</button>`;
    const cancelButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;

    // Set up the modal
    setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [deleteButton, cancelButton]);

    // Show the modal
    $('#myModal').modal('show');
    // Delete permission on click
    $('#deletePermission').off('click').on('click', async function () {
        // Implement the delete permission logic here
        const result = await removeRoleFromPermissionAsync({ roleId, permissionId });
        if (result.isSuccess) {
            $('#myModal').modal('hide');
            roleTable.ajax.reload();
        } else {
            showErrorModal(result.error.description);
        }
    });
});
