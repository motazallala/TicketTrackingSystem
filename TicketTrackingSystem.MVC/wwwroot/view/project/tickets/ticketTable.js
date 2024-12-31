import { initializeDataTable } from '../../../utility/dataTableUtility.js';
import { setupModalData } from '../../../utility/dataModalUtility.js';

let ticketTable;

$(document).ready(function () {
    let projectId = $('#projectId').val();


    ticketTable = initializeDataTable({
        tableId: '#ticketTable',
        apiUrl: 'https://localhost:7264/ticket/call',
        method: 'getallticketpaginatedasync',
        columns: [
            { data: 'id', name: 'ID' },
            { data: 'title', name: 'Title' },
            { data: 'description', name: 'Description' },
            { data: 'status', name: 'Status' },
            { 
                data: 'createdAt', 
                name: 'CreatedAt',
                render: function (data, type, row) {
                    return new Date(data).toLocaleString();
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

                //    // Optionally, you can add delete or other buttons based on permissions
                //    if (canDelete) {
                //        actionButtons += `
                //<button class="btn btn-danger btn-sm me-2 dt-delete">
                //    <i class="bi bi-trash-fill"></i> Delete
                //</button>`;
                //    }

                    // Close the div and return the HTML
                    actionButtons += `</div>`;

                    return actionButtons;
                }
            }
        ],
        additionalParameters: [projectId],
        failureCallback: showErrorMessage
    });

    // Row click event handler
    ticketTable.on('click', '.dt-view', function () {
        let data = ticketTable.row($(this).parents('tr')).data();
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
                <label for="title">Title :</label>
                <input type="text" class="form-control" id="title" name="title" value="${data.title}" disabled>
            </div>
            <div class="form-group">
                <label for="description">Description :</label>
                <input type="text" class="form-control" id="description" name="description" value="${data.description}" disabled>
            </div>
            <div class="form-group">
                <label for="status">Status :</label>
                <input type="text" class="form-control" id="status" name="status" value="${data.status}" disabled>
            </div>
            <div class="form-group">
                <label for="createdAt">Created At :</label>
                <input type="text" class="form-control" id="createdAt" name="createdAt" value="${new Date(data.createdAt).toLocaleString()}" disabled>
            </div>
            <div class="form-group">
                <label for="message">Message :</label>
                <textarea class="form-control" id="message" name="message" disabled>${data.message}</textarea>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Project/Details/${data.id}">View</a>`;
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
export { ticketTable };
