import { initializeDataTable } from '../../utility/dataTableUtility.js';
import { setupModalData } from '../../utility/dataModalUtility.js';

let roleTable;

$(document).ready(function () {
    roleTable = initializeDataTable({
        tableId: '#roleTable',
        apiUrl: 'https://localhost:7264/permission/call',
        method: 'getallroleswithpermissionpaginatedasync',
        ordering: false,
        columns: [
            { data: 'id', name: 'ID', orderable: false, },
            { data: 'name', name: 'Name' },
            {
                data: 'permissions', // Assuming this contains the array of permissions
                orderable: false, // Disable sorting on this column
                name: 'Permission Name',
                render: function (data, type, row) {
                    if (data && Array.isArray(data)) {
                        let groupedPermissions = '';
                        for (let i = 0; i < data.length; i += 4) {
                            groupedPermissions += `<div>`;
                            groupedPermissions += `<span class="badge bg-primary my-1 permission-badge" data-role-id="${row.id}" data-permission-id="${data[i].id}">${data[i].name}</span>`;
                            if (i + 1 < data.length) {
                                groupedPermissions += ` <span class="badge bg-primary my-1 permission-badge" data-role-id="${row.id}" data-permission-id="${data[i + 1].id}">${data[i + 1].name}</span>`;
                            }
                            if (i + 2 < data.length) {
                                groupedPermissions += ` <span class="badge bg-primary my-1 permission-badge" data-role-id="${row.id}" data-permission-id="${data[i + 2].id}">${data[i + 2].name}</span>`;
                            }
                            if (i + 3 < data.length) {
                                groupedPermissions += ` <span class="badge bg-primary my-1 permission-badge" data-role-id="${row.id}" data-permission-id="${data[i + 3].id}">${data[i + 3].name}</span>`;
                            }
                            groupedPermissions += `</div>`;
                        }
                        return groupedPermissions;
                    }
                    return '';
                }
            },
            {
                data: "id",
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    return `
                        <div class="d-flex justify-content-center">
                            <button class="btn btn-primary btn-sm me-2 dt-view">
                                <i class="bi bi-info-square-fill"></i> Permission Details
                            </button>
                        </div>
                    `;
                }
            }
        ],
        additionalParameters: null,
        failureCallback: showErrorMessage
    });



    // Row click event handler
    roleTable.on('click', '.dt-view', function () {
        let data = roleTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Role Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id-view" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name-view" name="name" value="${data.name}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Role/Details/${data.id}">View</a>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, moreDetails]);
        $('#myModal').modal('show');
    });

    // Function to handle errors
    function showErrorMessage(error) {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = `Error ${error.code}`;
        const bodyContent = `<p>${error.description}</p>`;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton]);
        $('#myModal').modal('show');
    }
    $('#myModal').on('hidden.bs.modal', function (evt) {
        $('.modal .modal-title').empty();
        $('.modal .modal-body').empty();
        $('.modal .modal-footer').empty();
    });
});

// Export the DataTable instance when it's ready
export { roleTable };
