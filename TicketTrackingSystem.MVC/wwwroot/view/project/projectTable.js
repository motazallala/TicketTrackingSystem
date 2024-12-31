import { initializeDataTable } from '../../utility/dataTableUtility.js';
import { setupModalData } from '../../utility/dataModalUtility.js';

let projectTable;

$(document).ready(function () {
    projectTable = initializeDataTable({
        tableId: '#projectTable',
        apiUrl: 'https://localhost:7264/project/call',
        method: 'getallprojectpaginatedasync',
        columns: [
            { data: 'id', name: 'ID' },
            { data: 'name', name: 'Name' },
            { data: 'description', name: 'Description' },
            { 
                data: 'createdAt', 
                name: 'CreatedAt',
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        return new Date(data).toLocaleString();
                    }
                    return data;
                }
            },
            {
                data: "id",
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    // Retrieve the values from the hidden inputs
                    var canView = $('#canView').val() === "true";
                    var canEdit = $('#canEdit').val() === "true";
                    var canDelete = $('#canDelete').val() === "true";

                    // Start building the action buttons HTML
                    var actionButtons = `<div class="d-flex justify-content-center">`;

                    // Add the 'Details' button if viewing is allowed
                    if (canView) {
                        actionButtons += `
                <button class="btn btn-primary btn-sm me-2 dt-view">
                    <i class="bi bi-info-square-fill"></i> Details
                </button>`;
                    }

                    // Add the 'Edit' button if editing is allowed
                    if (canEdit) {
                        actionButtons += `
                <button class="btn btn-warning btn-sm me-2 dt-edit">
                    <i class="bi bi-pencil-square"></i> Edit
                </button>`;
                    }

                    // Optionally, you can add delete or other buttons based on permissions
                    if (canDelete) {
                        actionButtons += `
                <button class="btn btn-danger btn-sm me-2 dt-delete">
                    <i class="bi bi-trash-fill"></i> Delete
                </button>`;
                    }

                    // Close the div and return the HTML
                    actionButtons += `</div>`;

                    return actionButtons;
                }
            }
        ],
        additionalParameters: null,
        failureCallback: showErrorMessage
    });

    // Row click event handler
    projectTable.on('click', '.dt-view', function () {
        let data = projectTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Project Details';
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
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Project/Details/${data.id}">View Project Users</a>`;
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
export { projectTable };
