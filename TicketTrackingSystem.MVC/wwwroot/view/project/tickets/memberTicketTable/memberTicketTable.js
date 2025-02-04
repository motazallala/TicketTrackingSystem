﻿import { initializeDataTable, reinitializeDataTable } from '../../../../utility/dataTableUtility.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { getAllNotSeenMessageForTicketAsync } from '../../../../services/ticketMessageService.js';


let memberTicketTable;

$(document).ready(function () {
    let projectId = $('#projectId').val();
    let reserved = true;

    // Initialize the DataTable on page load
    memberTicketTable = initializeDataTable({
        tableId: '#memberTicketTable',
        apiUrl: 'https://localhost:7264/ticket/call',
        method: 'getallticketformemberpaginatedasync',
        columns: getDataTableColumns(reserved),
        additionalParameters: [projectId, reserved],
        failureCallback: showErrorMessage
    });


    // Reinitialize DataTable based on toggle
    function reinitializeTable(projectId, reserved) {
        memberTicketTable = reinitializeDataTable({
            tableId: '#memberTicketTable',
            apiUrl: 'https://localhost:7264/ticket/call',
            method: 'getallticketformemberpaginatedasync',
            columns: getDataTableColumns(reserved),
            additionalParameters: [projectId, reserved],
            failureCallback: showErrorMessage
        });
    }

    // Event delegation for handling click events dynamically
    $(document).on('click', '.dt-view', function () {
        const row = memberTicketTable.row($(this).closest('tr'));

        if (row && row.data()) {
            showTicketDetailsModal(row.data());
        }
    });

    // Toggle between "My Tickets" and "All Tickets"
    $('#reinitializeBtn').on('click', function () {
        this.innerHTML = !reserved ? 'My Tickets' : 'All Tickets';
        reserved = !reserved;

        reinitializeTable(projectId, reserved);
    });

    // Common function to get DataTable columns with the reserved toggle
    function getDataTableColumns(reserved) {
        return [
            { data: 'id', name: 'ID' },
            { data: 'title', name: 'Title' },
            { data: 'description', name: 'Description' },
            { data: 'status', name: 'Status' },
            {
                data: 'createdAt',
                name: 'CreatedAt',
                render: function (data, type, row) {
                    return data ? new Date(data).toLocaleString() : '';
                }
            },
            {
                data: 'id',
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    if (!row || !row.id) return '';

                    let canView = $('#canView').val() === "true";
                    let canEdit = $('#canEdit').val() === "true";
                    let actionButtons = `<div class="d-flex justify-content-center">`;

                    if (canView) {
                        actionButtons += `<button class="btn btn-primary btn-sm me-2 dt-view">
                            <i class="bi bi-info-square-fill"></i> Details
                        </button>`;

                    }


                    if (reserved) {
                        if (canEdit) {
                            actionButtons += `<button class="btn btn-warning btn-sm me-2 dt-edit">
                                <i class="bi bi-pencil-square"></i> Edit
                            </button>`;
                            actionButtons += `<button class="btn btn-warning btn-sm me-2 dt-reAssign">
                                <i class="bi bi-pencil-square"></i> Re-Assign
                            </button>`;
                            actionButtons += `<button class="btn btn-warning btn-sm me-2 dt-removeAssign">
                            <i class="bi bi-info-square-fill"></i> Remove Assign
                            </button>`;
                        }
                    } else {
                        actionButtons += `<button class="btn btn-warning btn-sm me-2 dt-assign">
                            <i class="bi bi-pencil-square"></i> Assign to Me
                        </button>`;
                    }

                    actionButtons += `</div>`;
                    return actionButtons;
                }
            }
        ];
    }

    // Modal setup for showing ticket details
    async function showTicketDetailsModal(data) {
        if (!data) return;
        // getAllNotSeenMessageForTicketAsync and get the count of not seen messages
        let notSeenMessageResult = await getAllNotSeenMessageForTicketAsync(data.id);
        let notSeenMessageCount = 0;
        try {
            if (notSeenMessageResult.isSuccess) {
                notSeenMessageCount = notSeenMessageResult.data;
            } else {
                showErrorModal(updateResult.error.description);
            }
        } catch (e) {
            showErrorModal('There is an error that happened!');
        }
        const title = 'Ticket Details';
        const bodyContent = `
            <div class="form-group">
                <label for="id">ID:</label>
                <input type="text" class="form-control" value="${data.id || ''}" disabled>
            </div>
            <div class="form-group">
                <label for="title">Title:</label>
                <input type="text" class="form-control" value="${data.title || ''}" disabled>
            </div>
            <div class="form-group">
                <label for="description">Description:</label>
                <input type="text" class="form-control" value="${data.description || ''}" disabled>
            </div>
            <div class="form-group">
                <label for="status">Status:</label>
                <input type="text" class="form-control" value="${data.status || ''}" disabled>
            </div>
            <div class="form-group">
                <label for="createdAt">Created At:</label>
                <input type="text" class="form-control" value="${data.createdAt ? new Date(data.createdAt).toLocaleString() : ''}" disabled>
            </div>
        `;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const removeAssingButton = `<button class="btn btn-warning btn-sm me-2 dt-removeAssign">
                            <i class="bi bi-info-square-fill"></i> Remove Assign
                        </button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/project/ticket/${data.id}/messages">View Ticket Message (${notSeenMessageCount})</a>`;
        const buttons = data.status !== "Returned" ? [closeButton, removeAssingButton, moreDetails] : [closeButton, moreDetails];
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, buttons);

        $('#myModal').modal('show');
    }

    // Show error message modal
    function showErrorMessage(error) {
        if (!error) return;

        const title = `Error ${error.code || ''}`;
        const bodyContent = `<p>${error.description || 'An error occurred'}</p>`;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        setupModalData($('.modal .modal-title'), $('#modelBody'), $('.modal .modal-footer'), title, bodyContent, [closeButton]);

        $('#myModal').modal('show');
    }
});

export { memberTicketTable };