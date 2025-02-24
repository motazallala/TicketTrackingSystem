import { initializeDataTableAjax } from '../../../../utility/dataTableUtility.js';
import { setupModalData, showErrorModal } from '../../../../utility/dataModalUtility.js';
import { getAllNotSeenMessageForTicketAsync } from '../../../../services/ticketMessageService.js';


let memberTicketTable;

$(document).ready(function () {
    let projectId = $('#projectId').val();
    let reserved = true;

    // Initialize the DataTable on page load
    memberTicketTable = initializeDataTableAjax({
        tableId: '#memberTicketTable',
        apiUrl: 'https://localhost:7264/ticket/getallticketformemberpaginatedasync',
        method: 'getallticketformemberpaginatedasync',
        columns: getDataTableColumns(reserved),
        additionalParameters: () => ({ projectId, reserved }),
        failureCallback: showErrorMessage
    });



    // Toggle between showing users with and without roles
    $('#reinitializeBtn').on('click', function () {
        this.innerHTML = !reserved ? 'My Tickets' : 'All Tickets';
        reserved = !reserved;

        reloadTableWithFilters();
    });

    // Reload DataTable with filters (without reinitializing the table)
    function reloadTableWithFilters() {
        if ($.fn.DataTable.isDataTable('#memberTicketTable')) {
            // Destroy the existing DataTable
            memberTicketTable.destroy();
        }

        // Reinitialize the DataTable with updated columns based on userWithRole
        memberTicketTable = initializeDataTableAjax({
            tableId: '#memberTicketTable',
            apiUrl: 'https://localhost:7264/ticket/getallticketformemberpaginatedasync',
            columns: getDataTableColumns(reserved),  // Dynamically get columns based on toggle
            additionalParameters: () => ({ projectId, reserved }),
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


    // Common function to get DataTable columns with the reserved toggle
    function getDataTableColumns(reserved) {
        return [
            //{ data: 'id', name: 'ID' },
            { data: 'title', name: 'Title' },
            { data: 'creatorName', name: 'CreatorName' },
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
                        actionButtons += `<button class="btn btn-primary btn-sm me-2 d-flex flex-column dt-view">
                            <i class="bi bi-info-square-fill"></i> <div class="text-nowrap">Details</div>
                        </button>`;

                    }


                    if (reserved) {
                        if (canEdit) {
                            actionButtons += `<button class="btn btn-info btn-sm me-2 d-flex flex-column dt-edit">
                                <i class="bi bi-pencil-square"></i> <div class="text-nowrap">Action</div>
                            </button>`;
                            actionButtons += `<button class="btn btn-warning btn-sm me-2 d-flex flex-column dt-reAssign">
                                <i class="bi bi-arrow-repeat"></i> <div class="text-nowrap">Re-Assign</div>
                            </button>`;
                            actionButtons += `<button class="btn btn-danger btn-sm me-2 d-flex flex-column dt-removeAssign">
                            <i class="bi bi-info-square-fill"></i> <div class="text-nowrap">Remove Assign</div>
                            </button>`;
                        }
                    } else {
                        actionButtons += `<button class="btn btn-warning btn-sm me-2 d-flex flex-column dt-assign">
                            <i class="bi bi-pencil-square"></i> <div class="text-nowrap">Assign to Me</div>
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
                <textarea class="form-control" id="description" name="description" style="height: 200px;" disabled>${data.description || ''}</textarea>
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

        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/project/ticket/${data.id}/messages">View Ticket Message (${notSeenMessageCount})</a>`;
        const buttons = [closeButton, moreDetails];
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